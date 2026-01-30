# Ludu Arts – Unity Developer Intern Case  
## World Interaction System (Third Person)

**Aday:** Yusuf Mert ÜLGEN
**Pozisyon:** Unity Developer Intern  
**Oyun Perspektifi:** Third Person  
**Unity Versiyonu:** Unity 6.0.58f2
**Teslim Türü:** GitHub Repository  

---

## Genel Bakış

Bu projede, **third-person bir oyuncu karakterinin** dünya içindeki nesnelerle
etkileşime geçebileceği **modüler, genişletilebilir ve performans odaklı** bir
**World Interaction System** geliştirilmiştir.

Sistem; oyuncu algılama, etkileşim yaşam döngüsü, farklı interactable türleri,
basit bir envanter ve kullanıcı arayüzü geri bildirimlerinden oluşur.

Proje boyunca:
- Ludu Arts C# ve Unity standartlarına uyulmuştur
- Kod okunabilirliği ve sürdürülebilirlik önceliklendirilmiştir
- Interaction flow baştan sona tutarlı şekilde ele alınmıştır

---

## Mimari Yaklaşım

Sistem aşağıdaki ana katmanlardan oluşur:
Core
├── IInteractable
├── InteractionContext

Player
├── InteractionDetector
├── PlayerInteractionController
├── PlayerMovementController
└── PlayerLookController (3rd Person Camera)

Interactables
├── DoorInteractable
├── ChestInteractable
├── KeyPickupInteractable
└── SwitchInteractable

Inventory
├── PlayerInventory
└── ItemDefinition (ScriptableObject)

UI
└── InteractionUIController
└── InventoryUIController


Her katman **tek sorumluluk prensibi** ile tasarlanmıştır.

---

## Core Interaction System

### IInteractable

Tüm etkileşime girilebilir nesneler `IInteractable` arayüzünü implement eder.

Bu arayüz:
- Etkileşim mesafesini
- Etkileşim noktasını
- Etkileşim yapılabilirliğini
- Etkileşim yaşam döngüsünü

standart hale getirir.

**Etkileşim Yaşam Döngüsü:**
1. `CanInteract`
2. `BeginInteraction`
3. `UpdateInteraction`
4. `EndInteraction`

---

### InteractionContext

`InteractionContext`, etkileşim sırasında interactable’lara aktarılan
**immutable** bir veri yapısıdır.

İçerdiği bilgiler:
- Interactor (oyuncu) transform’u
- Delta time
- Input state (press / hold / release)

Bu sayede:
- Interactable’lar input okumaz
- Input logic player tarafında kalır
- Etkileşim deterministik olur

---

## Player Sistemleri

### InteractionDetector

Oyuncunun yakınındaki interactable nesneleri algılar.

**Özellikler:**
- `Physics.OverlapSphereNonAlloc` kullanır
- Allocation yapmaz
- Throttled çalışır (her frame değil)
- Aynı anda sadece **en yakın** interactable seçilir

Bu yapı, third-person kamerada bile performanslı ve stabil bir algılama sağlar.

---

### PlayerInteractionController

Etkileşim sisteminin **merkez kontrolcüsüdür**.

**Görevleri:**
- Input okuma (E tuşu)
- InteractionContext oluşturma
- Interaction lifecycle yönetimi
- Başarısız etkileşimleri UI’a bildirme

Detector ile input arasındaki zamanlama problemlerini önlemek için
**son algılanan interactable cache’lenir**.

---

### PlayerMovementController

Third-person karakter hareketini yönetir.

**Özellikler:**
- `CharacterController` tabanlı
- WASD hareket
- Kamera yönüne göre hareket
- Gravity ve grounded kontrolü

Interaction sisteminden tamamen bağımsızdır.

---

### PlayerLookController (Third Person)

Third-person kamera bakışını yönetir.

**Özellikler:**
- Mouse ile kamera kontrolü
- Dikey açı clamp
- Player body yaw + camera pitch ayrımı
- Cursor lock

Bu controller, interaction sisteminden bağımsız çalışır.

---

## Inventory Sistemi

### ItemDefinition

`ScriptableObject` tabanlı item tanımıdır.

İçerik:
- Unique Item ID
- Display Name
- UI Icon

Runtime logic içermez, sadece data taşır.

---

### PlayerInventory

Oyuncuya ait runtime inventory sistemidir.

**Özellikler:**
- `HashSet` + `List` kombinasyonu
- Hızlı lookup
- UI için sıralı erişim
- `OnInventoryChanged` event’i

Kapılar ve sandıklar anahtar kontrolünü buradan yapar.

---

## Interactable Nesneler

### DoorInteractable (Kapı)

- Toggle tabanlı çalışır
- Açık / kapalı state
- Opsiyonel anahtar gereksinimi
- External trigger (switch) ile açılabilir

Third-person ortamda hem doğrudan etkileşim
hem de event-based kontrol destekler.

---

### ChestInteractable (Sandık)

- Hold interaction (basılı tutma)
- Konfigüre edilebilir süre
- İçinden item verir
- Bir kere açılabilir

Hold süresi UI üzerinden progress bar ile gösterilir.

---

### KeyPickupInteractable (Anahtar)

- Instant interaction
- Envantere item ekler
- Alındıktan sonra kendini devre dışı bırakır

Third-person oyunlar için sade ve net bir pickup örneğidir.

---

### SwitchInteractable (Switch / Lever)

- Toggle interaction
- `UnityEvent<bool>` ile event-based çalışır
- Door gibi nesneleri tetikleyebilir

Loose coupling sayesinde inspector’dan kolayca bağlanabilir.

---

## UI Feedback Sistemi

### InteractionUIController

Tüm interaction UI geri bildirimlerini yönetir.

**Görevleri:**
- Interaction prompt gösterimi
- Başarısız etkileşim mesajları
- Hold progress bar

**Davranışlar:**
- Fail message aktifken prompt gizlenir
- Prompt, algılanan interactable’a göre değişir
- Chest için hold progress gösterilir

UI sadece **okuyucudur**, gameplay logic içermez.

---

## Interaction Flow (Özet)

1. InteractionDetector en yakın interactable’ı bulur
2. PlayerInteractionController input okur
3. InteractionContext oluşturulur
4. CanInteract kontrol edilir
5. Interaction lifecycle çalıştırılır
6. UI duruma göre güncellenir

---

## Ludu Arts Standartlarına Uyum

Bu projede:

- `m_`, `s_`, `k_` prefix’leri kullanılmıştır
- Region sıralaması standartlara uygundur
- Public API’ler XML documentation içerir
- Silent bypass yapılmamış, hatalar loglanmıştır
- ScriptableObject ve prefab isimlendirmeleri kurallara uygundur

---

## Bilinen Limitasyonlar

- Animation entegrasyonu yapılmamıştır (hook’lar hazır)
- Save / Load sistemi yoktur
- Interaction highlight sistemi eklenmemiştir

---

## Ekstra Notlar

- Sistem yeni interactable’lar eklenmeye uygundur
- Third-person kamera ile sorunsuz çalışır
- Performans mobil platformlara uygundur

---

## Sonuç

Bu proje, third-person bir oyun için
**temiz, anlaşılır ve genişletilebilir**
bir World Interaction System sunar.

Ludu Arts coding standartlarına uyum gözetilerek,
gerçek üretim senaryolarına yakın şekilde geliştirilmiştir.

