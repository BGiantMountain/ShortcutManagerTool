# Changelog / Sürüm Notları

Bütün önemli detaylar, eklentiler ve yamalar burada belgelenmektedir. / All notable changes to this project will be documented in this file.

## [v2.1.0] - 2025-03-08

### 🚀 Eklendi (Added)
- **TR:** Kısayol Tarayıcısı (Shortcut Scanner) sekmesine eşlenmemiş (unmapped) özel ağ yollarını `\\Sunucu\Paylasim` formatında manuel olarak girebilme ve tarayabilme özelliği eklendi.
- **EN:** Added the ability to manually input and scan unmapped custom network paths (e.g., `\\Server\Share`) in the Shortcut Scanner tab.
- **TR:** Uygulama arayüzüne tam Türkçe (TR) ve İngilizce (EN) dil desteği eklendi. Sağ üst köşeden anında dil değişimi yapılabiliyor.
- **EN:** Added full Turkish (TR) and English (EN) language support to the application interface with an instant toggle switch.
- **TR:** Kısayol Tarayıcısı (Shortcut Scanner) sekmesi eklendi. Sistem disklerini veya ağ yollarını tarayıp içindeki kısayolları listeler.
- **EN:** Added Shortcut Scanner tab. It scans system drives or network paths and lists the shortcuts inside.
- **TR:** Klasörü "Gizli" (Hidden) yapıp bir üst dizine otomatik kısayolunu kopyalama fonksiyonu eklendi.
- **EN:** Added functionality to make a folder "Hidden" and automatically copy its shortcut to the parent directory.
- **TR:** Liste içerisindeki uygulamalar için çoklu özel isimlendirme (Custom Naming) desteği geldi. Aynı anda farklı isimlerde kısayollar atabilirsiniz.
- **EN:** Added multiple custom naming support for applications in the list. You can create shortcuts with different names simultaneously.

### 💅 Değiştirildi ve İyileştirildi (Changed & Improved)
- **TR:** Arayüz (UI) tamamen profesyonel Gri ve Antrasit (Dark Gray) temasına geçirildi.
- **EN:** The user interface (UI) has been completely redesigned with a professional Gray and Anthracite (Dark Gray) theme.
- **TR:** "Başarıyla Oluşturuldu" bildirimleri can sıkıcı pop-uplar yerine alt durum çubuğunda (StatusBar) zarif bir yeşil metin ve zamanlayıcı ile gösterilecek şekilde yenilendi.
- **EN:** "Successfully Created" notifications have been redesigned to show as an elegant green text with a timer in the StatusBar instead of annoying pop-ups.
- **TR:** Uygulamanın adı uluslararası isim olan "Shortcut Manager" olarak her iki dilde de sabitlendi ve çeviri sorunu giderildi.
- **EN:** Application name was fixed as "Shortcut Manager" in both languages and the translation override issue was resolved.
- **TR:** XAML arayüzündeki tüm etiketlerin (Label) İngilizce çeviri kapsamı %100'e çıkarıldı.
- **EN:** Achieved 100% English translation coverage for all static UI labels in the XAML interface.
- **TR:** Kısayol tarayıcısı sekmesindeki yeni ComboBox kutusu, beyaz varsayılan renginden arındırılarak bütünüyle Koyu Temaya (Dark Theme) uyarlandı.
- **EN:** The new ComboBox in the Shortcut Scanner tab was completely adapted to the Dark Theme, replacing its default white appearance.
- **TR:** Uygulamanın derleme yöntemi (`Framework-Dependent`, `PublishSingleFile`) optimize edilerek 140 MB'lık dosya boyutu **404 KB** seviyesine düşürüldü.
- **EN:** The application's compilation method was optimized (`Framework-Dependent`, `PublishSingleFile`), reducing the file size from ~140 MB down to just **404 KB**.

## [v2.0.0] - 2025-03-08

### 🚀 Eklendi (Added)
- **TR:** Sanal Mod (Virtual Run): İşletim sisteminde iz bırakmamak adına izole edilmiş yerel bir geçici hedefe dosyaları kopyalayıp çalıştıran yeni bir "Sanal Kısayol" altyapısı eklendi.
- **EN:** Virtual Run: Added a new "Virtual Shortcut" infrastructure that copies files to an isolated local temporary target to avoid leaving traces on the OS.
- **TR:** Toplu İşlemler & Alt Klasör Taraması: Seçilen klasörde belirli bir derinliğe kadar olan tüm `.exe` dosyalarını bulup aynı anda masaüstüne kısayol gönderme özelliği.
- **EN:** Bulk Operations & Subfolder Scanning: Ability to find all `.exe` files up to a certain depth in the selected folder and send shortcuts to the desktop simultaneously.
- **TR:** Anlık Filtreleme: Arama/filtreleme kutusu entegre edildi.
- **EN:** Instant Filtering: Integrated a live search/filtering box.
- **TR:** Özel İkon (Custom Icon) Belirleme.
- **EN:** Custom Icon Assignment capability.

### 🗑️ Kaldırıldı (Removed)
- **TR:** Ayarları Yönet Sekmesi tamamen silindi. Sadece "Kısayol oluşturmaya ve onarmaya" odaklanıldı.
- **EN:** Manage Settings Tab has been completely removed to focus solely on "Creating and repairing shortcuts".
