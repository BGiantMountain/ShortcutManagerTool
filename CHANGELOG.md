# Changelog / Sürüm Notları

Bütün önemli detaylar, eklentiler ve yamalar burada belgelenmektedir.

## [v2.0.0] - 2025-03-08

### 🚀 Eklendi (Added)
- **Sanal Mod (Virtual Run):** Ağ üzerinden çalışan veya "portable" olan `.exe`'leri sistemde iz bırakmamak adına izole edilmiş yerel bir geçici hedefe (`AppData/Local/ShortcutManager_VirtualApps`) kopyalayıp çalıştıran yeni bir "Sanal Kısayol" altyapısı eklendi. Uygulama kapatıldığında geçici dosyalar PowerShell tarafından otomatik olarak silinir.
- **Toplu İşlemler & Alt Klasör Taraması:** Seçilen klasörde belirli bir derinliğe (1-5 seviye) kadar olan tüm `.exe` dosyalarını bulup aynı anda masaüstüne sınırsız kısayol gönderme özelliği.
- **Anlık Filtreleme:** Çok yoğun dizinlerdeki `.exe` yığınlarını ayırt etmek için canlı çalışan arama/filtreleme kutusu entegre edildi.
- **Özel İkon (Custom Icon) Belirleme:** Kullanıcının kısayollar için varsayılan uygulama ikonu yerine özel bir `.ico` dosyası veya farklı bir `.exe` dosyasını kaynak ikon olarak belirleyebilme esnekliği.

### 💅 Değiştirildi ve İyileştirildi (Changed & Improved)
- **Tamamen Yeni Premium Arayüz (UI/UX Redesign):**
  - Tüm arayüz **Glassmorphism** (Cam efekti) hissi, modern renk geçişleri (Gradient) ve çok koyu uzay konseptli derinlik hissiyle sıfırdan tasarlandı.
  - Tıklanan tüm butonlarda hover durumuna göre alevlenen **Glow/Parlama** animasyonları dahil edildi.
  - Yazıların kesilmesini veya sıkışmasını engelleyen genişletilmiş "Card (Kart)" tasarım düzenleri kullanıldı ve giriş kutuları merkeze (`VerticalContentAlignment="Center"`) hizalanarak okunabilirlik zirveye taşındı.
- **Form Optimizasyonu:** Sanal Mod onay kutusu ve "Kısayol Adı" metin alanı sıkışıklıktan kurtarılarak tamamen asimetrik, geniş ve göze hoş gelen bir yan yana form düzenine sokuldu.
- **Yüksek Oranda Boyut Optimizasyonu (Compression):** `.NET 8 Self-Contained` sebebiyle ~162 MB boyutunda olan yürütülebilir `.exe` doysyasının derleme aşamasına `<EnableCompressionInSingleFile>` komutları eklenerek boyutunda **%55** oranında küçülme (~71.9 MB) sağlandı ve performans artırıldı.

### 🗑️ Kaldırıldı (Removed)
- **Ayarları Yönet Sekmesi:** Core projenin yalnızca "Kısayol oluşturmaya ve onarmaya" odaklanması adına eskiden var olan karmaşık "Ayar Senkronizasyon (Backup & Restore)" sistemi tamamen silindi.
- Gereksiz dış bağımlılıklar ve `VBScript` zorunlulukları modern `PowerShell` otomasyonlarıyla değiştirilip kökten kaldırıldı.
