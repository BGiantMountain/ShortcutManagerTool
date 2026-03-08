# 🔗 Akıllı Kısayol Merkezi — Kullanım Kılavuzu

**Akıllı Kısayol Merkezi**, masaüstü kısayollarını kolayca oluşturmanıza ve mevcut bozuk kısayolları onarmanıza olanak sağlayan bir Windows uygulamasıdır.

---

## 📋 Gereksinimler

- Windows 10 / 11
- .NET 8.0 Runtime (yoksa [buradan indirin](https://dotnet.microsoft.com/en-us/download/dotnet/8.0))

---

## 🖥️ Arayüze Genel Bakış

Uygulama iki sekmeden oluşur:

| Sekme | Amaç |
|---|---|
| 🛠️ Yeni Kısayol Üret | Bir klasördeki .exe dosyaları için masaüstü kısayolu oluşturur |
| ✏️ Kısayol Onar | Bozulmuş veya güncel olmayan mevcut bir .lnk dosyasını düzenler |

---

## 🛠️ Yeni Kısayol Üret

### Adım 1 — Uygulama Klasörünü Seçin

- **"Klasör Seç..."** butonuna tıklayın ve uygulamanın bulunduğu klasörü seçin.
- Alternatif olarak klasör yolunu doğrudan metin kutusuna yapıştırabilirsiniz.
- Ağ (UNC) yolları da desteklenir: `\\SunucuAdı\Paylaşım\UygulamaKlasörü`

> Klasör seçildiğinde, içindeki tüm `.exe` dosyaları otomatik olarak ağaç görünümünde listelenir.

### Adım 2 — Derinlik ve Arama

- **Derinlik Kaydırıcısı (1–5):** Klasör içinde kaç alt seviyeye kadar `.exe` aranacağını belirler. Varsayılan: 3.
- **Ara Kutusu:** Listelenen dosyalar arasında anlık arama yapmanızı sağlar.

### Adım 3 — Exe Dosyasını Seçin

- Ağaç görünümünden kısayolunu oluşturmak istediğiniz `.exe` dosyasının yanındaki **onay kutusunu** işaretleyin.
- Birden fazla `.exe` seçerek tek seferde birden fazla kısayol oluşturabilirsiniz.

### Adım 4 — Seçenekler

#### 🔴 Sanal Mod
- Bu seçenek işaretlendiğinde uygulama, program her çalıştırıldığında:
  1. Kaynak klasörü yerel bilgisayara geçici olarak kopyalar.
  2. Uygulamayı yerel kopyadan çalıştırır.
  3. Program kapandığında geçici kopyayı otomatik siler.
- **Ne zaman kullanılır?** Ağ sürücüsündeki bir uygulama doğrudan ağdan çalışmıyorsa veya uyumluluk sorunları yaşanıyorsa.
- Geçici dosyalar şuraya kaydedilir: `%LocalAppData%\ShortcutManager_VirtualApps\`

> ⚠️ Sanal Mod ile oluşturulan kısayol, uygulamayı her açışta kopyalama işlemi yapacağından açılış süresi uzayabilir.

### Adım 5 — Kısayol Adı Öneki

- Masaüstünde oluşturulacak kısayolun adını belirler.
- Boş bırakılırsa `.exe` dosyasının adı kullanılır.
- Birden fazla `.exe` seçildiğinde: `Önek - ExeAdı` formatında adlandırılır.

### Adım 6 — Özel İkon (İsteğe Bağlı)

- **"İkon Seç..."** butonuyla `.ico` veya başka bir `.exe` dosyasını ikon kaynağı olarak seçebilirsiniz.
- Boş bırakılırsa kısayolun ikonu olarak hedef `.exe` dosyasının kendi ikonu kullanılır.

### Adım 7 — Oluştur

- **"🚀 Masaüstüne Kısayol Oluştur"** butonuna tıklayın.
- İşlem başarılıysa masaüstünüzde yeni kısayol(lar) belirir.

---

## ✏️ Kısayol Onar

Mevcut bir `.lnk` dosyasının hedef yolu veya başlangıç dizini değişmişse bu sekmeyle düzeltebilirsiniz.

### Adım 1 — Kısayolu Seçin
- **"Kısayol Seç..."** butonuyla onarmak istediğiniz `.lnk` dosyasını seçin.
- Mevcut **Hedef Yol** ve **Başlangıç Yeri** otomatik olarak yüklenir.

### Adım 2 — Yeni Değerleri Girin
- **Yeni Hedef Yol:** Uygulamanın yeni `.exe` konumu.
- **Yeni Başlangıç Yeri:** Genellikle `exe` dosyasının bulunduğu klasör.

### Adım 3 — Kaydet
- **"💾 Değişiklikleri Kaydet"** butonuna tıklayın.
- Kısayol yerinde güncellenir, silip yeniden oluşturmanıza gerek kalmaz.

---

## 📁 Uygulama Dosya Konumları

| Amaç | Konum |
|---|---|
| Sanal Mod launcher scriptleri | `%AppData%\ShortcutManager\Launchers\` |
| Sanal Mod geçici uygulama kopyaları | `%LocalAppData%\ShortcutManager_VirtualApps\` |

---

## ❓ Sık Karşılaşılan Durumlar

**Kısayol oluşturuldu ama ikon görünmüyor:**
> Masaüstüne sağ tıklayıp "Yenile" yapın veya birkaç saniye bekleyin; Windows ikon önbelleğini günceller.

**Sanal Mod kısayolu çalıştırıldığında siyah bir pencere kısa süre açılıp kapanıyor:**
> Bu normaldir. PowerShell scripti arka planda kopyalama işlemi yapıyor. Uygulama kısa süre içinde başlayacaktır.

**Ağ yolundaki klasör görünmüyor:**
> UNC yolunu (`\\sunucu\klasör`) doğrudan metin kutusuna yapıştırın; "Klasör Seç..." dialogu ağ konumlarını her zaman göstermeyebilir.
