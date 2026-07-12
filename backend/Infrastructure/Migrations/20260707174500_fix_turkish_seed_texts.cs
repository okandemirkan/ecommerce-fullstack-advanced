using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ECommerceDbContext))]
    [Migration("20260707174500_fix_turkish_seed_texts")]
    public partial class fix_turkish_seed_texts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE "Users" SET "Username" = CASE "Id"
                    WHEN 101 THEN 'Ayşe Yılmaz' WHEN 105 THEN 'Zeynep Şahin' WHEN 106 THEN 'Emre Çelik'
                    WHEN 107 THEN 'Derya Aydın' WHEN 108 THEN 'Kerem Yıldız' WHEN 109 THEN 'Selin Koç'
                    WHEN 113 THEN 'Nehir Özkan' WHEN 114 THEN 'Can Yalçın' WHEN 117 THEN 'Seda Güneş'
                    WHEN 119 THEN 'Mina Taş' WHEN 120 THEN 'Ali Doğan' WHEN 121 THEN 'Yağmur Deniz'
                    WHEN 123 THEN 'İrem Sarı' WHEN 129 THEN 'Nazlı Çakır' WHEN 130 THEN 'Tuna Yücel'
                    ELSE "Username"
                END
                WHERE "Id" BETWEEN 101 AND 130;

                UPDATE "Products" SET "Description" = CASE "Id"
                    WHEN 101 THEN 'Günlük kullanım için güçlü kamera ve uzun pil ömrü sunan akıllı telefon.'
                    WHEN 102 THEN 'Yüksek yenileme hızlı ekran ve hızlı şarj destekli performans telefonu.'
                    WHEN 103 THEN 'A15 Bionic işlemci, kaliteli kamera ve uzun yazılım desteğiyle iPhone deneyimi.'
                    WHEN 104 THEN 'Hafif gövde, verimli işlemci ve günlük işler için ideal dizüstü bilgisayar.'
                    WHEN 105 THEN 'OLED ekranlı, kompakt ve premium ultrabook deneyimi.'
                    WHEN 106 THEN 'Tablet esnekliği ve laptop gücünü bir araya getiren hibrit cihaz.'
                    WHEN 107 THEN 'Oyun ve üretkenlik sistemleri için yüksek frekanslı DDR5 bellek kiti.'
                    WHEN 108 THEN 'PCIe 4.0 destekli yüksek hızlı NVMe depolama birimi.'
                    WHEN 109 THEN 'Yüksek FPS ve ray tracing performansı için güçlü ekran kartı.'
                    WHEN 110 THEN 'Aktif gürültü engelleme ve uzun pil ömrü sunan kablosuz kulaklık.'
                    WHEN 111 THEN 'Kompakt tasarımlı, güçlü ses veren taşınabilir Bluetooth hoparlör.'
                    WHEN 112 THEN 'Premium ses kalitesi ve gelişmiş gürültü engelleme özellikleri.'
                    WHEN 113 THEN 'OLED panel, düşük gecikme ve sinematik görüntü deneyimi.'
                    WHEN 114 THEN 'Yüksek yenileme hızlı kavisli oyuncu monitörü.'
                    WHEN 115 THEN 'Televizyonlara 4K akıllı yayın özelliği kazandıran kompakt medya oynatıcı.'
                    WHEN 116 THEN 'Hızlı otofokus ve kaliteli video özellikleriyle aynasız kamera.'
                    WHEN 117 THEN 'Aksiyon çekimleri için dayanıklı ve yüksek kaliteli kompakt kamera.'
                    WHEN 118 THEN 'Portre ve günlük çekimler için hafif ve keskin prime lens.'
                    WHEN 119 THEN 'Türk kahvesi hazırlamak için kompakt ve pratik kahve makinesi.'
                    WHEN 120 THEN 'Az yağlı pişirme ve ızgara özelliği sunan sıcak hava fritözü.'
                    WHEN 121 THEN 'Güçlü emiş ve akıllı haritalama özelliğiyle robot süpürge.'
                    WHEN 122 THEN '4K oyun deneyimi ve hızlı yükleme süreleri sunan oyun konsolu.'
                    WHEN 123 THEN 'Canlı OLED ekranlı, elde ve televizyonda oynanabilen oyun konsolu.'
                    WHEN 124 THEN 'Profesyonel oyuncular için hafif kablosuz gaming mouse.'
                    WHEN 125 THEN 'Sağlık takip özellikleri ve uzun pil ömrü sunan akıllı saat.'
                    WHEN 126 THEN 'Şık tasarımlı, spor ve sağlık takip özellikli akıllı saat.'
                    WHEN 127 THEN 'Android telefonlarla uyumlu kapsamlı akıllı saat deneyimi.'
                    WHEN 128 THEN 'Yüksek kapasiteli, hızlı şarj destekli taşınabilir güç kaynağı.'
                    WHEN 129 THEN 'Laptoplar için çoklu port ve monitör bağlantısı sunan USB C dock.'
                    WHEN 130 THEN 'WiFi 6 destekli, yüksek kapsama alanına sahip kablosuz router.'
                    ELSE "Description"
                END
                WHERE "Id" BETWEEN 101 AND 130;

                UPDATE "Addresses"
                SET
                    "City" = CASE "City"
                        WHEN 'Istanbul' THEN 'İstanbul' WHEN 'Izmir' THEN 'İzmir' WHEN 'Eskisehir' THEN 'Eskişehir'
                        WHEN 'Balikesir' THEN 'Balıkesir' WHEN 'Mugla' THEN 'Muğla' WHEN 'Tekirdag' THEN 'Tekirdağ'
                        WHEN 'Aydin' THEN 'Aydın' ELSE "City"
                    END,
                    "District" = CASE "District"
                        WHEN 'Kadikoy' THEN 'Kadıköy' WHEN 'Cankaya' THEN 'Çankaya' WHEN 'Karsiyaka' THEN 'Karşıyaka'
                        WHEN 'Nilufer' THEN 'Nilüfer' WHEN 'Muratpasa' THEN 'Muratpaşa' WHEN 'Tepebasi' THEN 'Tepebaşı'
                        WHEN 'Selcuklu' THEN 'Selçuklu' WHEN 'Yildirim' THEN 'Yıldırım' WHEN 'Odunpazari' THEN 'Odunpazarı'
                        WHEN 'Balcova' THEN 'Balçova' WHEN 'Bayrakli' THEN 'Bayraklı' WHEN 'Kusadasi' THEN 'Kuşadası'
                        ELSE "District"
                    END,
                    "FullAddress" = replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace("FullAddress",
                        'Ataturk Bulvari', 'Atatürk Bulvarı'), 'Universite Caddesi', 'Üniversite Caddesi'),
                        'Yazir Mahallesi', 'Yazır Mahallesi'), 'Ziyapasa Bulvari', 'Ziyapaşa Bulvarı'),
                        'Tunali Hilmi', 'Tunalı Hilmi'), 'Gorukle', 'Görükle'),
                        'Akcay', 'Akçay'), 'Isiklar', 'Işıklar'), 'Bahcelievler', 'Bahçelievler'),
                        'Sumer Mahallesi', 'Sümer Mahallesi'), 'Masukiye Mahallesi', 'Maşukiye Mahallesi')
                WHERE "Id" BETWEEN 301 AND 360;

                UPDATE "Orders" SET "ShippingAddress" = CASE "Id"
                    WHEN 1001 THEN 'İstanbul, Kadıköy, Moda Caddesi No:12'
                    WHEN 1002 THEN 'Ankara, Çankaya, Atatürk Bulvarı No:48'
                    WHEN 1003 THEN 'İzmir, Karşıyaka, Bahariye Sokak No:7'
                    WHEN 1004 THEN 'Bursa, Nilüfer, Cumhuriyet Mahallesi No:22'
                    WHEN 1005 THEN 'Antalya, Muratpaşa, Lara Caddesi No:5'
                    WHEN 1006 THEN 'Eskişehir, Tepebaşı, Üniversite Caddesi No:31'
                    WHEN 1007 THEN 'Konya, Selçuklu, Yazır Mahallesi No:19'
                    WHEN 1008 THEN 'Kocaeli, İzmit, Fethiye Caddesi No:3'
                    WHEN 1010 THEN 'Adana, Seyhan, Ziyapaşa Bulvarı No:27'
                    WHEN 1011 THEN 'Mersin, Yenişehir, Gazi Mustafa Kemal Bulvarı No:8'
                    WHEN 1014 THEN 'Balıkesir, Altıeylül, Milli Kuvvetler Caddesi No:14'
                    WHEN 1015 THEN 'Muğla, Menteşe, Cumhuriyet Sokak No:6'
                    WHEN 1016 THEN 'İstanbul, Beşiktaş, Abbasağa Mahallesi No:18'
                    WHEN 1017 THEN 'Ankara, Keçiören, Fatih Caddesi No:63'
                    WHEN 1018 THEN 'İzmir, Bornova, Kazım Dirik Mahallesi No:29'
                    WHEN 1019 THEN 'Bursa, Osmangazi, Altıparmak Caddesi No:41'
                    WHEN 1020 THEN 'Antalya, Konyaaltı, Akdeniz Bulvarı No:72'
                    WHEN 1021 THEN 'Edirne, Merkez, Saraçlar Caddesi No:16'
                    WHEN 1022 THEN 'Gaziantep, Şahinbey, İnönü Caddesi No:58'
                    WHEN 1023 THEN 'Sakarya, Serdivan, Arabacıalanı No:23'
                    WHEN 1024 THEN 'Tekirdağ, Süleymanpaşa, Hürriyet Mahallesi No:9'
                    WHEN 1025 THEN 'Denizli, Pamukkale, Çamlık Caddesi No:32'
                    ELSE "ShippingAddress"
                END
                WHERE "Id" BETWEEN 1001 AND 1025;

                UPDATE "Reviews" SET "Comment" = CASE "Id"
                    WHEN 3001 THEN 'Kamera performansı ve ekran kalitesi beklentimin üstünde.'
                    WHEN 3003 THEN 'Gürültü engellemesi başarılı, uzun kullanımda da rahat.'
                    WHEN 3004 THEN 'Performansı iyi, özellikle iş akışında oldukça hızlı.'
                    WHEN 3006 THEN 'Görüntü kalitesi çok canlı, salon için ideal oldu.'
                    WHEN 3007 THEN 'Ekranı ve kalem deneyimi çok iyi.'
                    WHEN 3009 THEN 'Aksiyon çekimlerinde görüntü sabitlemesi başarılı.'
                    WHEN 3010 THEN 'Renk doğruluğu iyi, tasarım işleri için memnun kaldım.'
                    WHEN 3012 THEN 'Fotoğraf kalitesi güçlü, gövde ergonomisi de iyi.'
                    WHEN 3013 THEN 'Pil süresi ve sağlık takibi gayet başarılı.'
                    WHEN 3015 THEN 'Kurulumu kolay, oyun performansı beklediğim gibi.'
                    WHEN 3016 THEN 'Ses seviyesi güçlü, baslar temiz geliyor.'
                    WHEN 3018 THEN 'Haritalama düzgün, günlük temizlik için yeterli.'
                    WHEN 3019 THEN 'Yeni başlayanlar için pratik ve kaliteli bir gövde.'
                    WHEN 3021 THEN 'Günlük kullanımda hızlı, ekran parlaklığı yeterli.'
                    WHEN 3023 THEN 'Kamera ve yazılım deneyimi istikrarlı.'
                    WHEN 3024 THEN 'Hafif ve sessiz çalışıyor, ofis işleri için ideal.'
                    WHEN 3026 THEN 'Tablet ve laptop arasında iyi bir denge sunuyor.'
                    WHEN 3027 THEN 'Sistem yükseltmesi sonrası performans farkı hissediliyor.'
                    WHEN 3029 THEN 'Oyunlarda yüksek ayarlarda akıcı sonuç aldım.'
                    WHEN 3030 THEN 'Kulaklık rahat, bağlantısı stabil.'
                    WHEN 3032 THEN 'Ses kalitesi temiz, pil süresi tatmin edici.'
                    WHEN 3033 THEN 'Siyah seviyesi ve kontrast gerçekten etkileyici.'
                    WHEN 3035 THEN 'Eski televizyonu pratik şekilde akıllı hale getirdi.'
                    WHEN 3036 THEN 'Otofokus hızı ve video kalitesi güçlü.'
                    WHEN 3038 THEN 'Portre çekimleri için keskin ve hafif bir lens.'
                    WHEN 3039 THEN 'Kahveyi hızlı hazırlıyor, temizliği kolay.'
                    WHEN 3041 THEN 'Emiş gücü iyi, uygulama üzerinden yönetimi kolay.'
                    WHEN 3042 THEN 'Yükleme süreleri kısa, oyun deneyimi akıcı.'
                    WHEN 3044 THEN 'Çok hafif, hassasiyeti oyunlarda başarılı.'
                    WHEN 3045 THEN 'Spor takibi detaylı, pil süresi iyi.'
                    WHEN 3047 THEN 'Telefonla uyumu sorunsuz, ekranı parlak.'
                    WHEN 3048 THEN 'Kapasitesi yüksek, hızlı şarj desteği iş görüyor.'
                    WHEN 3050 THEN 'Kapsama alanı güçlü, kurulum kısa sürdü.'
                    ELSE "Comment"
                END
                WHERE "Id" BETWEEN 3001 AND 3050 AND "Comment" IS NOT NULL;
                """
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
