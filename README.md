# FaturaYonetimSistemi
### Sistemin arayüzü .Net 5 MVC kullanılarak yazıldı.
### Sistemin Yönetimi ve işlemleri için MS SQL kullanıldı.

### Arayüz dışında kullanıcıların kredi kartı ile ödeme yapabilmesi için bir .Net5 WebApi servisi mevcut.
Bu serviste sistemde ki her bir kullanıcı için banka bilgileri(bakiye, kredi kartı no vb.) MongoDB aracılığıyla tutularak MS SQL'den ayrı bir şekilde tutuluyor 
ve WebApi ya çağırılarak orda doğrulanıyor ve işlemlerimiz gerçekleştiriliyor.

### Admin
* Daire bilgilerini girebilir.
* İkamet eden kullanıcı bilgilerini girer.
* Daire başına ödenmesi gereken aidat ve fatura bilgilerini girer(Aylık olarak). Toplu veya tek tek atama yapılabilir.
* Gelen ödeme bilgilerini görür.
* Gelen mesajları görür.
* Aylık olarak borç-alacak listesini görür.
* Kişileri listeler, düzenler, siler.
* Daire/konut bilgilerini listeler, düzenler siler.

### Kullanıcı
* Kendisine atanan fatura ve aidat bilgilerini görür.
* Kredi kartı ile ödeme yapabilir.
* Yöneticiye mesaj gönderebilir.
* Daire/Konut bilgilerinde
* Hangi blokda
* Durumu (Dolu-boş)
* Tipi (2+1 vb.)
* Bulunduğu kat
* Daire numarası
* Daire sahibi veya kiracı
* Kullanıcı bilgilerinde
* Ad-soyad
* TCNo
* E-Mail
* Telefon
* Araç bilgisi

### Sistem kullanılmaya başladığında ilk olarak
* Yönetici daire bilgilerini girer.
* Kullanıcı bilgilerini girer.Giriş yapması için otomatik olarak bir şifre oluşturulur.
* Kullanıcıları dairelere atar
* Ay bazlı olarak aidat bilgilerini girer.
* Ay bazlı olarak fatura bilgilerini girer.
