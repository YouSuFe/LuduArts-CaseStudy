# World Interaction System

## Genel Bilgi

Bu proje, **Ludu Arts Unity Developer Intern Case** kapsamında geliştirilmiş
modüler bir **World Interaction System** örneğidir.

Oyuncu, dünya içerisindeki farklı nesnelerle
(door, chest, switch, key vb.)
etkileşime girebilir.

Proje, okunabilirlik, genişletilebilirlik ve
Ludu Arts kodlama standartlarına uyum gözetilerek geliştirilmiştir.

---

## Kullanılan Unity Versiyonu

- **Unity:** 6.0.58f2

---

## Nasıl Çalıştırılır

1. Projeyi Unity Hub üzerinden açın
2. `Assets/[ProjectName]/Scenes/TestScene.unity` sahnesini açın
3. Play butonuna basın

---

## Kontroller

| Input | Açıklama |
|------|----------|
| **W / A / S / D** | Oyuncu hareketi |
| **Mouse** | Kamera kontrolü (3rd Person) |
| **E** | Etkileşim |
| **E (Basılı Tut)** | Hold interaction (örn: chest açma) |

---

## Oynanış ve Etkileşimler

- Oyuncu, etkileşime geçilebilir nesnelere yaklaştığında
  ekranda bir **interaction prompt** görür
- Gerekli menzil içindeyken **E** tuşuna basılarak etkileşim başlatılır
- Bazı nesneler:
  - **Anında etkileşim** (Key Pickup)
  - **Toggle etkileşim** (Door, Switch)
  - **Hold etkileşim** (Chest)

---

## Mevcut Interactable Nesneler

- **Door**
  - Açılıp kapanabilir
  - Anahtar gerektirebilir
- **Key Pickup**
  - Envantere eklenir
- **Switch / Lever**
  - Event tabanlı çalışır
  - Kapı gibi nesneleri tetikleyebilir
- **Chest**
  - Basılı tutularak açılır
  - Açıldıktan sonra tekrar kullanılamaz

---

## UI Geri Bildirimleri

- Interaction prompt (örn: *Press E to Open*)
- Fail message (örn: *Key Required*)
- Hold interaction için progress bar

---

## Notlar

- Proje **3rd Person kamera** ile oynanır
- Interaction sistemi modüler yapıdadır
- Yeni interactable türleri kolayca eklenebilir
- Kodlar Ludu Arts C# ve naming standartlarına uygun şekilde yazılmıştır

---

## Bilinen Limitasyonlar

- Save / Load sistemi bulunmamaktadır
- Animasyonlar opsiyonel olarak eklenebilir
- UI tasarımı minimal tutulmuştur

---

## Ek Bilgi

LLM (ChatGPT) kullanımı detaylı şekilde
**PROMPTS.md** dosyasında belgelenmiştir.
