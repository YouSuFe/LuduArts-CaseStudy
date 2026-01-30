# LLM Kullanım Dokümantasyonu

## Özet

- **Toplam prompt sayısı:** 10
- **Kullanılan araçlar:** ChatGPT
- **En çok yardım alınan konular:**
  - Case gereksinimlerinin analizi
  - Ludu Arts kurallarının yorumlanması
  - Interaction system için mimari planlama
  - Detection yöntemi seçimi (Raycast vs Overlap)
  - Script geliştirme sırası ve ilk iskelet kodların çıkarılması
  - Interaction logic testleri ve bug çözüm yaklaşımları
  - Interactable object scriptlerinin ilk implementasyonu
  - Interaction UI bileşenlerinin tasarlanması
  - Scriptlerin eksiklerinin giderilmesi ve final hale getirilmesi
  - Proje dokümantasyonunun hazırlanması (.md dosyaları)

Bu projede LLM,
**geliştirme sürecinin tamamına yayılmış bir destek aracı**
olarak kullanılmıştır.
Kod yazımından mimariye, testten dokümantasyona kadar
birçok aşamada bilinçli şekilde faydalanılmıştır.

---

## Prompt 0: Case analizi ve proje planlaması

**Araç:** ChatGPT

**Prompt (Özet):**
> Ludu Arts tarafından verilen kural dosyalarını paylaşıp,
> bu case’de nelere dikkat etmem gerektiğini,
> projenin ne anlatmak istediğini,
> nasıl bir interaction system beklendiğini
> ve adım adım nasıl bir implementation planı izlenebileceğini sordum.

**Alınan Cevap (Özet):**
> - Kural dosyaları özetlendi ve önemli maddeler vurgulandı  
> - Case’in temel amacının modüler ve genişletilebilir bir interaction system olduğu açıklandı  
> - Core system, interactable’lar, UI ve inventory için adım adım bir geliştirme planı sunuldu  

**Nasıl Kullandım:**
- [ ] Direkt kullandım  
- [x] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> Alınan cevaptaki mimari ve adım adım plan,
> proje için bir başlangıç rehberi olarak kullanıldı.
> Ancak implementasyon detayları ve teknik kararlar
> proje ilerledikçe geliştirici tarafından şekillendirildi.

---

## Prompt 1: Interaction detection yöntemi seçimi (Raycast vs OverlapSphere)

**Araç:** ChatGPT

**Prompt (Özet):**
> Interaction detection için raycast mi yoksa overlap sphere mi
> daha verimli olur diye bir teknik tartışma başlattım.

**Alınan Cevap (Özet):**
> - OverlapSphere yaklaşımının bu case için daha esnek olduğu  
> - Raycast’in daha yön-bağımlı ve sınırlı kaldığı  
> - Throttled ve allocation-free kullanımın avantajları  

**Nasıl Kullandım:**
- [x] Direkt kullandım  
- [ ] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> Bu öneri doğrudan mimari karara dönüştürüldü
> ve InteractionDetector OverlapSphere tabanlı olarak yazıldı.

---

## Prompt 2: Implementation sırası ve ilk script iskeleti

**Araç:** ChatGPT

**Prompt (Özet):**
> Genel planı gördükten sonra
> hangi script ile başlamamız gerektiğini sordum
> ve en baştan sona adım adım scriptleri birlikte oluşturmaya başladık.

**Alınan Cevap (Özet):**
> - İlk yazılması gereken core class belirlendi  
> - Yazım standardı ve temel iskelet gösterildi  
> - Diğer scriptlerin sırası netleştirildi  

**Nasıl Kullandım:**
- [x] Direkt kullandım  
- [ ] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> İlk aşamada amaç sistemin çalışır hale gelmesiydi.
> Eksikler fark edilse de bu aşamada değiştirilmedi,
> final aşamasında ele alınmak üzere not edildi.

---

## Prompt 3: Interaction logic testleri ve bug çözümü

**Araç:** ChatGPT

**Prompt (Özet):**
> Interaction logic için gerekli birkaç ana script tamamlandıktan sonra
> testler yaptım ve karşılaştığım bug’ları anlatarak
> olası nedenlerini ve çözümlerini sordum.

**Alınan Cevap (Özet):**
> - State yönetimi problemleri açıklandı  
> - Interaction lifecycle edge-case’leri belirtildi  
> - Birden fazla çözüm yaklaşımı önerildi  

**Nasıl Kullandım:**
- [ ] Direkt kullandım  
- [x] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> Öneriler birebir kopyalanmadı.
> Bug’ların temel nedenleri analiz edilerek
> projeye uygun çözümler geliştirildi.

---

## Prompt 4: Interactable object scriptlerinin oluşturulması

**Araç:** ChatGPT

**Prompt (Özet):**
> Door, Key, Chest ve Switch gibi
> 4 farklı interactable object için
> örnek scriptler oluşturulmasını istedim.

**Alınan Cevap (Özet):**
> - Temel interactable scriptleri üretildi  
> - Interaction lifecycle ile uyumlu şekilde kurgulandı  

**Nasıl Kullandım:**
- [x] Direkt kullandım  
- [ ] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> Bu aşamada öncelik sistemin uçtan uca çalışmasıydı.
> Scriptlerin eksikleri olduğu bilinerek
> doğrudan kullanıldı.

---

## Prompt 5: Interaction UI bileşenlerinin oluşturulması

**Araç:** ChatGPT

**Prompt (Özet):**
> Interaction prompt, fail message ve hold progress gibi
> UI bileşenlerini anlattım ve
> bunları yöneten bir UI controller scripti yazmasını istedim.

**Alınan Cevap (Özet):**
> - Interaction UI controller scripti oluşturuldu  
> - UI ile gameplay logic ayrımı vurgulandı  

**Nasıl Kullandım:**
- [x] Direkt kullandım  
- [ ] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> UI tarafı hızlıca ayağa kaldırıldı.
> Daha sonra null-check ve güvenlik iyileştirmeleri yapıldı.

---

## Prompt 6,7,8,9: Scriptlerin eksiklerinin giderilmesi ve final hale getirilmesi

**Araç:** ChatGPT

**Prompt (Özet):**
> Tüm scriptler yazıldıktan sonra
> tek tek eksik gördüğüm noktaları,
> edge-case’leri ve kendi gereksinimlerimi anlatarak
> scriptlerin tamamlanmasını istedim.
>
> Bu süreç birden fazla iterasyon halinde ilerledi.

**Alınan Cevap (Özet):**
> - Eksik alanlar tespit edildi  
> - Bazı methodlar genişletildi veya düzenlendi  
> - Null-safety, state kontrolü ve okunabilirlik iyileştirildi  

**Nasıl Kullandım:**
- [ ] Direkt kullandım  
- [x] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> Bu aşamada LLM bir “tamamlayıcı” rol üstlendi.
> Önerilen değişiklikler birebir kopyalanmadı,
> proje ihtiyaçlarına göre uyarlanarak uygulandı.
> Bu iterasyonlar sonucunda proje son haline ulaştı.

## Prompt 10: Proje dokümantasyonunun hazırlanması (.md dosyaları)

**Araç:** ChatGPT

**Prompt (Özet):**
> Projede kullanılan interaction system’i,
> mimari kararları, teknik detayları ve kullanım adımlarını anlatan
> README.md ve diğer gerekli `.md` dokümanlarının
> benim adıma hazırlanmasını istedim.

**Alınan Cevap (Özet):**
> - Proje yapısını ve interaction system’i anlatan
>   kapsamlı Markdown dokümanları oluşturuldu  
> - README.md içeriği case gereksinimlerine uygun şekilde yazıldı  
> - Teknik kararlar ve kullanım adımları net biçimde açıklandı  

**Nasıl Kullandım:**
- [x] Direkt kullandım  
- [ ] Adapte ettim  
- [ ] Reddettim  

**Açıklama:**
> Oluşturulan `.md` dosyaları,
> proje dokümantasyonu olarak doğrudan repository’ye eklendi.
> Bu aşamada amaç, geliştirilen sistemi
> reviewer’ın kolayca anlayabileceği ve test edebileceği
> net ve düzenli bir dokümantasyon sunmaktı.