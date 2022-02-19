using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje3
{
    class Program
    {
        public static int TOPLAM_MUSTERI = 20;
        public static Random random = new Random();

        static void Main(string[] args)
        {
            string[] duraklar = new string[9]; ///uzunluğu 9 olan duraklar dizisi boyutlandırılarak oluşturuluyor.
            Tree DurakAgaci = new Tree(); /// DurakAgaci adında boş bir Binary Search Tree oluşturuluyor.

            ////////// 1 - a) //////////
            DurakAgaciOLustur(duraklar, DurakAgaci); /// duraklar dizisi ve DurakAgaci argüman olarak gönderilip önce duraklar dizisi dolduruluyor, ardından DurakAgaci'na eklemeler yapılıyor.

            ////////// 1 - b) //////////
            AgacBilgileriniYazdir(DurakAgaci, duraklar.Length);

            ////////// 1 - c) //////////
            Console.Write("\n\n\nKiralama işlemleri hakkında bilgi almak istediğiniz müşterinin ID'sini giriniz (0 ile 20 arasında) : ");
            KiralamaBilgileriniYazdir(DurakAgaci, int.Parse(Console.ReadLine()));

            ////////// 1 - d) //////////
            Console.Write("\n\n\nKiralama işlemi yapılacak istasyon adını giriniz : ");
            string durak = Console.ReadLine();
            Console.Write("\n\n\nKiralama işlemi yapacak müşteri ID'sini giriniz (0 ile 20 arasında) : ");
            int ID = int.Parse(Console.ReadLine());
            KiralamaİslemiYap(DurakAgaci, durak, ID);

            ////////// 2 - a) //////////
            Console.Write("\n\n\n2. sorunun a) şıkkının cevabı için ENTER'a basınız : ");
            Console.ReadLine();
            Hashtable durakTable = HashTableOlustur(duraklar);

            ////////// 2 - b) //////////
            Console.Write("\n\n\n2. sorunun b) şıkkının cevabı için ENTER'a basınız : ");
            Console.ReadLine();
            HashTableGuncelle(durakTable, duraklar);

            ////////// 3 - b) //////////
            Console.Write("\n\n\n3. sorunun b) şıkkının cevabı için ENTER'a basınız : ");
            Console.ReadLine();
            Heap heap = MaxHeapOlustur(duraklar);

            ////////// 3 - c) //////////
            Console.Write("\n\n\n3. sorunun c) şıkkının cevabı için ENTER'a basınız : ");
            Console.ReadLine();
            EnFazlaNormalBisikletOlan3IstasyonuYazdir(heap, duraklar);


            ////////// 4 - a) //////////
            Console.Write("\n\n\n4. sorunun a) şıkkının cevabı için ENTER'a basınız : ");
            Console.ReadLine();
            BosParkSayisinaGoreBubbleSort(duraklar);

            ////////// 4 - b) //////////
            Console.Write("\n\n\n4. sorunun b) şıkkının cevabı için ENTER'a basınız : ");
            Console.ReadLine();
            NormalBisikletSayisinaGoreQuickSort(duraklar);

            Console.ReadKey();
        }

        private static void DurakAgaciOLustur(string[] duraklar, Tree DurakAgaci)
        {
            ///duraklar dizisi 9'a tamamlanıyor.
            duraklar[0] = "İnciraltı, 28, 2, 10";
            duraklar[1] = "Sahilevleri, 8, 1, 11";
            duraklar[2] = "Doğal Yaşam Parkı, 17, 1, 6";
            duraklar[3] = "Bostanlı İskele, 7, 0, 5";
            duraklar[4] = "Yunuslar, 7, 1, 4";
            duraklar[5] = "Tersane Cafe (Alaybey), 7, 1, 4";
            duraklar[6] = "Fuar Montrö, 9, 1, 5";
            duraklar[7] = "Pasaport İskele, 12, 1, 7";
            duraklar[8] = "Ahmet Adnan Saygun, 7, 1, 12";


            int i = 0;
            int sayac = 0;
            List<Musteri> MusteriListesi; /// Musteri nesnelerini tutacak olan generic listenin referansı oluşturuluyor.
            string[] sub; ///duraklar dizisinin her bir ögesi sahalarına ayrıştırılarak bu geçici alt diziye atılacak.
            while (i < duraklar.Length)
            {
                while (sayac < TOPLAM_MUSTERI)
                {
                    MusteriListesi = new List<Musteri>();
                    sub = duraklar[i].Split(',');  /// duraklar dizisinin her bir ögesi ',' karakterinden kırılarak alt dizine aktarılır.

                    int genericListLength = random.Next(1, 11); /// 1 ile 10 arasında random sayı oluşturuluyor. MusteriListesi'nin uzunluğu bu random sayı kadardır.
                    for (int j = 0; j < genericListLength; j++) /// Her bir Durak nesnesini ağaca eklerken, düğüme List tipinde bir veri yapısı içinde 1 ile 10 adet arasında random sayıda rastgele Müşteri (Müşteri ID, kiralama saati) ekleme işlemi bu döngü içinde yapılıyor.
                    {
                        int duraktakiBosParkSayisi = int.Parse(sub[1].Replace(" ", ""));
                        int duraktakiTandemBisikletSayisi = int.Parse(sub[2].Replace(" ", ""));
                        int duraktakiNormalBisikletSayisi = int.Parse(sub[3].Replace(" ", ""));
                        int tip = random.Next(0, 2); ///Müşterinin aldığı bisikletin tipi rastgele belirleniyor (0= tandem bisiklet, 1= normal bisiklet arasından rastgele belirlenir)  
                        int saat = random.Next(0, 24); ///Müşterinin bisikleti aldığı saat rastgele belirleniyor (istasyonların 7/24 esasına göre çalıştığı kabul edilmiştir.) (0 ile 24 arasında) (24 dahil değil)
                        int dakika = random.Next(0, 60); ///Müşterinin bisikleti aldığı dakika rastgele belirleniyor (0 ile 59 arasında) 

                        if (tip == 0 && duraktakiTandemBisikletSayisi != 0)  ///tip 0, yani tandem bisiklet ise ve aynı anda o duraktaki tandem bisiklet sayısı 0 değilse yeni bir müşteri oluşturulur ve MusteriListesi'ne eklenir.
                        {
                            MusteriListesi.Add(new Musteri(random.Next(0, 21), saat, dakika, tip)); ///0 ile 20 arasında rastgele ID ve kiralama saati, kiralama dakikası, kiralanan bisiklet tipi ile Musteri nesnesi oluşturulur
                            sub[1] = " " + (duraktakiBosParkSayisi + 1).ToString(); ///Duraktaki boş park sayısı 1 artırılır.
                            sub[2] = " " + (duraktakiTandemBisikletSayisi - 1).ToString();  ///Duraktaki tandem bisiklet sayısı 1 azaltılır.
                            sayac += 1;
                        }
                        else if (tip == 1 && duraktakiNormalBisikletSayisi != 0)  ///tip 1, yani normal bisiklet ise ve aynı anda o duraktaki normal bisiklet sayısı 0 değilse yeni bir müşteri oluşturulur ve MusteriListesi'ne eklenir.
                        {
                            MusteriListesi.Add(new Musteri(random.Next(0, 21), saat, dakika, tip)); ///0 ile 20 arasında rastgele ID ve kiralama saati, kiralama dakikası, kiralanan bisiklet tipi ile Musteri nesnesi oluşturulur
                            sub[1] = " " + (duraktakiBosParkSayisi + 1).ToString(); ///Duraktaki boş park sayısı 1 artırılır.
                            sub[3] = " " + (duraktakiNormalBisikletSayisi - 1).ToString();  ///Duraktaki normal bisiklet sayısı 1 azaltılır.
                            sayac += 1;
                        }

                        if (sayac == TOPLAM_MUSTERI) break;  /// toplam müşteri sayısına erişildikten sonra bu döngü sonlandırılır. Artık diğer duraklara 0 müşteri atanır. Yani o duraklarda hiçbir müşteri işlem yapmadı diye kabul edilmiştir.
                    }
                    DurakAgaci.insert(new Durak(sub[0], int.Parse(sub[1].Replace(" ", "")), int.Parse(sub[2].Replace(" ", "")), int.Parse(sub[3].Replace(" ", "")), MusteriListesi));  ///alt dizinin ilk ögesi string tipinde kalır, diğerleri önce boşluktan arındırılarak daha sonrasında int tipine dönüştürülür
                    i += 1;
                    if (i == duraklar.Length) break;
                }
                i++;
                if (i == duraklar.Length) break; ///döngü kontrol değişkeni iki ayrı döngüde artırıldığı için, sayac değişkeninden önce maksimum değerine ulaşırsa diye böyle bir önlem aldım.
                MusteriListesi = new List<Musteri>();
                MusteriListesi.Add(new Musteri(-1, 0, 0, 0)); /// toplam müşteri sayısına erişildikten sonra artık diğer duraklara 0 müşteri atanır. Yani o duraklarda hiçbir müşteri işlem yapmadı diye kabul edilmiştir.

                sub = duraklar[i].Split(',');
                DurakAgaci.insert(new Durak(sub[0], int.Parse(sub[1].Replace(" ", "")), int.Parse(sub[2].Replace(" ", "")), int.Parse(sub[3].Replace(" ", "")), MusteriListesi));
            }
        }

        private static void AgacBilgileriniYazdir(Tree DurakAgaci, int treeSize)
        {
            DurakAgaci.findAndWriteTreeInfo(DurakAgaci.getRoot(), treeSize);

            Console.Write("\n\n\nAğacın InOrder dolaşılması : ");
            DurakAgaci.inOrder(DurakAgaci.getRoot());
            Console.Write("\n\n\nAğacın PreOrder dolaşılması : ");
            DurakAgaci.preOrder(DurakAgaci.getRoot());
            Console.Write("\n\n\nAğacın PostOrder dolaşılması : ");
            DurakAgaci.postOrder(DurakAgaci.getRoot());
        }

        private static void KiralamaBilgileriniYazdir(Tree DurakAgaci, int ID)
        {
            DurakAgaci.findByIDinTree(DurakAgaci.getRoot(), ID);
        }

        private static void KiralamaİslemiYap(Tree DurakAgaci, string durak, int ID)
        {
            DurakAgaci.kirala(DurakAgaci.getRoot(), durak, ID);
            KiralamaBilgileriniYazdir(DurakAgaci, ID);
        }

        private static Hashtable HashTableOlustur(string[] duraklar)
        {
            Hashtable durakTable = new Hashtable();
            string[] durakArray;
            int i = 0;
            while (i < duraklar.Length)
            {
                durakArray = duraklar[i].Split(',');
                string key = durakArray[0];
                List<int> values = new List<int>();
                values.Add(int.Parse(durakArray[1].Replace(" ", "")));
                values.Add(int.Parse(durakArray[2].Replace(" ", "")));
                values.Add(int.Parse(durakArray[3].Replace(" ", "")));
                durakTable.Add(key, values);
                i++;
            }

            Console.WriteLine("--Hashtable--");
            foreach (DictionaryEntry item in durakTable)
            {
                Console.Write($"Key: {item.Key} Value: ");
                List<int> numbers = (List<int>)item.Value;
                for (int n = 0; n < numbers.Count; n++)
                {
                    Console.Write(numbers[n]);
                    if (n != numbers.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine("");
            }

            return durakTable;
        }

        private static void HashTableGuncelle(Hashtable durakTable, string[] duraklar)
        {
            int i = 0;
            while (i < duraklar.Length)
            {
                string[] durakArray2 = duraklar[i].Split(',');
                string key = durakArray2[0];
                int bosPark = int.Parse(durakArray2[1].Replace(" ", ""));
                int normalBisiklet = int.Parse(durakArray2[3].Replace(" ", ""));
                if (bosPark > 5)
                {
                    bosPark -= 5;
                    normalBisiklet += 5;
                    List<int> values = new List<int>();
                    values.Add(bosPark);
                    values.Add(int.Parse(durakArray2[2].Replace(" ", "")));
                    values.Add(normalBisiklet);
                    durakTable[key] = values;
                }
                i++;
            }

            Console.WriteLine("--Hashtable--Updated--");
            foreach (DictionaryEntry item in durakTable)
            {
                Console.Write($"Key: {item.Key} Value: ");
                List<int> numbers = (List<int>)item.Value;
                for (int n = 0; n < numbers.Count; n++)
                {
                    Console.Write(numbers[n]);
                    if (n != numbers.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine("");
            }
        }

        private static Heap MaxHeapOlustur(string[] duraklar)
        {
            Heap heap = new Heap(31); // make a Heap; max size 31

            string[] durakArray3;
            int i = 0;
            while (i < duraklar.Length)
            {
                durakArray3 = duraklar[i].Split(',');
                heap.insert(int.Parse(durakArray3[3].Replace(" ", "")));
                i++;
            }

            heap.displayHeap();

            return heap;
        }

        private static void EnFazlaNormalBisikletOlan3IstasyonuYazdir(Heap heap, string[] duraklar)
        {
            int i = 0;
            List<string> enFazlaNormalBisiklet = new List<string>();
            while (i < 3)
            {
                HeapNode heapNode = heap.remove();
                foreach (string durak in duraklar)
                {
                    string[] durakArray4;
                    durakArray4 = durak.Split(',');
                    int normalBisikletSayisi = int.Parse(durakArray4[3].Replace(" ", ""));

                    if (heapNode.getKey() == normalBisikletSayisi)
                    {
                        enFazlaNormalBisiklet.Add(durak);
                    }
                }
                i++;
            }

            Console.WriteLine("En fazla normal bisiklete sahip durakların bilgisi : ");
            foreach (string enFazla in enFazlaNormalBisiklet)
            {
                Console.WriteLine(enFazla);
            }

            heap.displayHeap();
        }

        private static void BosParkSayisinaGoreBubbleSort(string[] duraklar)
        {
            int maxSize = 100; // array size
            BubbleSort bubbleArray = new BubbleSort(maxSize); // create the array
            foreach (string durak in duraklar)
            {
                string[] durakArray4;
                durakArray4 = durak.Split(',');
                int bosParkSayisi = int.Parse(durakArray4[1].Replace(" ", ""));
                bubbleArray.insert(bosParkSayisi);
            }
            bubbleArray.display(); // display items
            bubbleArray.bubbleSort(); // bubble sort them
            bubbleArray.display(); // display them again
        }

        private static void NormalBisikletSayisinaGoreQuickSort(string[] duraklar)
        {
            int maxSize = 16; // array size
            QuickSort quickArray = new QuickSort(maxSize); // create the array
            foreach (string durak in duraklar)
            {
                string[] durakArray4;
                durakArray4 = durak.Split(',');
                int normalBisikletSayisi = int.Parse(durakArray4[3].Replace(" ", ""));
                quickArray.insert(normalBisikletSayisi);

            }
            quickArray.display(); // display items
            quickArray.quickSort(); // quicksort them
            quickArray.display(); // display them again
        }
    }

    class BubbleSort
    {
        private int[] a; // ref to array a
        private int nElems; // number of data items

        public BubbleSort(int max) // constructor
        {
            a = new int[max]; // create the array
            nElems = 0; // no items yet
        }

        public void insert(int value) // put element into array
        {
            a[nElems] = value; // insert it
            nElems++; // increment size
        }

        public void display() // displays array contents
        {
            Console.WriteLine("");
            for (int j = 0; j < nElems; j++) // for each element,
                Console.Write(a[j] + " "); // display it
            Console.WriteLine("");
        }

        public void bubbleSort()
        {
            int outIndex, inIndex;
            for (outIndex = nElems - 1; outIndex > 1; outIndex--) // outer loop (backward)
            {
                for (inIndex = 0; inIndex < outIndex; inIndex++) // inner loop (forward)
                {
                    if (a[inIndex] > a[inIndex + 1]) // out of order?
                    {
                        swap(inIndex, inIndex + 1);
                    }
                }
            }
        }

        private void swap(int one, int two)
        {
            int temp = a[one];
            a[one] = a[two];
            a[two] = temp;
        }
    }

    class QuickSort
    {
        private int[] a; // ref to array a
        private int nElems; // number of data items

        public QuickSort(int max) // constructor
        {
            a = new int[max]; // create the array
            nElems = 0; // no items yet
        }

        public void insert(int value) // put element into array
        {
            a[nElems] = value; // insert it
            nElems++; // increment size
        }

        public void display() // displays array contents
        {
            Console.Write("A=");
            for (int j = 0; j < nElems; j++) // for each element,
                Console.Write(a[j] + " "); // display it
            Console.WriteLine("");
        }

        public void quickSort()
        {
            recQuickSort(0, nElems - 1);
        }

        private void recQuickSort(int left, int right)
        {
            if (right - left <= 0) // if size <= 1,
                return; // already sorted
            else // size is 2 or larger
            {
                long pivot = a[right]; // rightmost item
                                       // partition range
                int partition = partitionIt(left, right, pivot);
                recQuickSort(left, partition - 1); // sort left side
                recQuickSort(partition + 1, right); // sort right side
            }
        }

        private int partitionIt(int left, int right, long pivot)
        {
            int leftPtr = left - 1; // left (after ++)
            int rightPtr = right; // right-1 (after --)
            while (true)
            {
                while (a[++leftPtr] < pivot)
                    ; // (nop)
                      // find smaller item
                while (rightPtr > 0 && a[--rightPtr] > pivot)
                    ; // (nop)

                if (leftPtr >= rightPtr) // if pointers cross,
                    break; // partition done
                else // not crossed, so
                    swap(leftPtr, rightPtr); // swap elements
            }
            swap(leftPtr, right); // restore pivot
            return leftPtr; // return pivot location
        }

        private void swap(int dex1, int dex2) // swap two elements
        {
            int temp = a[dex1]; // A into temp
            a[dex1] = a[dex2]; // B into A
            a[dex2] = temp; // temp into B
        } // end swap(
    }

    class Heap
    {
        private HeapNode[] heapArray;
        private int maxSize;
        private int currentSize;

        public Heap(int mx) // Yığın sınıfının yapılandırıcısı
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new HeapNode[maxSize]; // Altyapıda kullanılacak dizinin boyutlandırılarak oluşturulması
        }

        public bool insert(int key) /// Yığına eleman ekleme metodu
        {
            if (currentSize == maxSize)
                return false;
            HeapNode newNode = new HeapNode(key);
            heapArray[currentSize] = newNode; ///eklenen eleman ilkin yığının en sonuna eklenir
            trickleUp(currentSize++); /// en sona eklenen eleman bu metot ile olması gerektiği yere çıkarılır. 
            return true;
        }

        public void trickleUp(int index)
        {
            int parent = (index - 1) / 2;
            HeapNode bottom = heapArray[index];
            while (index > 0 && heapArray[parent].getKey() < bottom.getKey())
            {
                heapArray[index] = heapArray[parent];
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }

        public HeapNode remove() // En büyük değerli nodu (Max Heap olduğu için root) silme işlemi
        { // (assumes non-empty list) ("boş olmayan bir liste olduğu kabul edilmiştir)
            HeapNode root = heapArray[0];
            heapArray[0] = heapArray[--currentSize]; ///en büyük değerli nod silindikten sonra sonuncu eleman en başa geçirilir.
            trickleDown(0); /// en başa geçirilen sonuncu eleman bu metot sayesinde olması gerektiği yere indirilir.
            return root;
        } 

        public void trickleDown(int index)
        {
            int largerChild;
            HeapNode top = heapArray[index]; // save root
            while (index < currentSize / 2) // while node has at
            { // least one child,
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                // find larger child
                if (rightChild < currentSize && // (rightChild exists?)
                heapArray[leftChild].getKey() <
                heapArray[rightChild].getKey())
                    largerChild = rightChild;
                else
                    largerChild = leftChild;
                // top >= largerChild?
                if (top.getKey() >= heapArray[largerChild].getKey())
                    break;
                // shift child up
                heapArray[index] = heapArray[largerChild];
                index = largerChild; // go down
            } // end while
            heapArray[index] = top; // root to index
        }

        public void displayHeap() /// yığını ekranan yazdıran metot
        {
            Console.Write("Heap Array: ");
            for (int m = 0; m < currentSize; m++)
                if (heapArray[m] != null)
                    Console.Write(heapArray[m].getKey() + " ");
                else
                    Console.Write("-- ");
            Console.WriteLine("");


            int nBlanks = 32;
            int itemsPerRow = 1;
            int column = 0;
            int j = 0;
            String dots = "...............................";
            Console.WriteLine(dots + dots);
            while (currentSize > 0)
            {
                if (column == 0)
                    for (int k = 0; k < nBlanks; k++)
                        Console.Write(" ");
                Console.Write(heapArray[j].getKey());
                if (++j == currentSize)
                    break;

                if (++column == itemsPerRow)
                {
                    nBlanks /= 2;
                    itemsPerRow *= 2;
                    column = 0;
                    Console.WriteLine("");
                }
                else
                    for (int k = 0; k < nBlanks * 2 - 2; k++)
                        Console.Write(" ");
            }
            Console.WriteLine("\n" + dots + dots);
        }
    }

    class HeapNode
    {
        private int data;
        public HeapNode(int key)
        {
            data = key;
        }
        public int getKey()
        {
            return data;
        }
        public void setKey(int id)
        {
            data = id;
        }
    }

    class Durak
    {
        public string DurakAdi;
        public int BosPark;
        public int TandemBisiklet;
        public int NormalBisiklet;
        public List<Musteri> MusteriList;

        public Durak(string DurakAdi, int BosPark, int TandemBisiklet, int NormalBisiklet, List<Musteri> MusteriList)
        {
            this.DurakAdi = DurakAdi;
            this.BosPark = BosPark;
            this.TandemBisiklet = TandemBisiklet;
            this.NormalBisiklet = NormalBisiklet;
            this.MusteriList = MusteriList;
        }
    }

    class Musteri
    {
        public int MusteriID;
        public int Saat;
        public int Dakika;
        public int Tip; /// 0 = Tandem Bisiklet, 1 = Normal Bisiklet. Her bir müşteri 1 bisiklet alıyor diye kabul edilmiştir.

        public Musteri(int MusteriID, int Saat, int Dakika, int Tip)
        {
            this.MusteriID = MusteriID;
            this.Saat = Saat;
            this.Dakika = Dakika;
            this.Tip = Tip;
        }

        //public override string ToString()
        //{
        //    return MusteriAdi + ", " + UrunSayisi;
        //}
    }

    // Düğüm Sınıfı
    class TreeNode
    {
        public Durak durak;
        public TreeNode leftChild;
        public TreeNode rightChild;

        public void kirala(string durakAdi, int ID)
        {
            if (durakAdi == durak.DurakAdi)
            {
                if (durak.NormalBisiklet != 0)
                {
                    Random random = new Random();
                    durak.NormalBisiklet -= 1;
                    durak.BosPark += 1;
                    int saat = random.Next(0, 24);
                    int dakika = random.Next(0, 60);
                    durak.MusteriList.Add(new Musteri(ID, saat, dakika, 1));
                }
                else Console.WriteLine("Bu durakta normal bisiklet kalmamıştır.");
            }
        }

        public void findByIDinNode(int ID)
        {
            foreach (Musteri musteri in durak.MusteriList)
            {
                if (musteri.MusteriID == ID)
                {
                    String saat, dakika, bisiklet, ek, bilgi;
                    if (musteri.Dakika < 10) dakika = "0" + musteri.Dakika;
                    else dakika = musteri.Dakika.ToString();
                    if (musteri.Saat < 10) saat = "0" + musteri.Saat;
                    else saat = musteri.Saat.ToString();
                    if (musteri.Tip == 0) bisiklet = "Tandem Bisiklet";
                    else bisiklet = "Normal Bisiklet";
                    if (musteri.Dakika % 10 == 0 || musteri.Dakika % 10 == 6 || musteri.Dakika % 10 == 9) ek = "'da";
                    else if (musteri.Dakika % 10 == 1 || musteri.Dakika % 10 == 2 || musteri.Dakika % 10 == 7 || musteri.Dakika % 10 == 8) ek = "'de";
                    else ek = "'te";

                    Console.WriteLine("\n" + ID + " Nolu müşteri " + durak.DurakAdi + " durağından " + saat + "." + dakika + ek + " " + bisiklet + " kiralamıştır");

                }
            }
        }

        public void displayNode()
        {
            Console.Write("\nDurak Adı : " + durak.DurakAdi + ", Boş Park Sayısı : " + durak.BosPark + ", Tandem Bisiklet Sayısı : " + durak.TandemBisiklet + ", Normal Bisiklet Sayısı :  " + durak.NormalBisiklet + " ");
            foreach (Musteri musteri in durak.MusteriList)
            {
                if (musteri.MusteriID == -1)
                {
                    Console.Write("\n\t\tBu durakta kiralama işlemi yapan müşteri yoktur.\n");
                }
                else
                {
                    String saat, dakika;
                    if (musteri.Dakika < 10) dakika = "0" + musteri.Dakika;
                    else dakika = musteri.Dakika.ToString();
                    if (musteri.Saat < 10) saat = "0" + musteri.Saat;
                    else saat = musteri.Saat.ToString();
                    Console.Write("\n\t\tMüşteri ID : " + musteri.MusteriID);
                    Console.Write(", Kiralama Saati : " + musteri.Saat + "." + musteri.Dakika);
                    if (musteri.Tip == 0) Console.Write(", Kiraladığı bisiklet : Tandem Bisiklet\n");
                    else if (musteri.Tip == 1) Console.Write(", Kiraladığı bisiklet : Normal Bisiklet\n");
                }

            }
        }
    }

    // Agaç Sınıfı
    class Tree
    {
        private TreeNode root;

        //variables for traverse statistics
        public int totalDepth;
        public int maxDepth;
        public int leavesDepthTotal;
        public int leavesCount;
        public int[] elementCountForEachDepth;
        public int[] sumElementValuesForEachDepth;

        public Tree() { root = null; }

        public TreeNode getRoot()
        { return root; }

        public void kirala(TreeNode localRoot, string durak, int ID)
        {
            if (localRoot != null)
            {
                localRoot.kirala(durak, ID);
                kirala(localRoot.leftChild, durak, ID);
                kirala(localRoot.rightChild, durak, ID);
            }
        }

        public void findByIDinTree(TreeNode localRoot, int ID)
        {
            if (localRoot != null)
            {
                localRoot.findByIDinNode(ID);
                findByIDinTree(localRoot.leftChild, ID);
                findByIDinTree(localRoot.rightChild, ID);
            }
        }

        // Agacın preOrder Dolasılması
        public void preOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.displayNode();
                preOrder(localRoot.leftChild);
                preOrder(localRoot.rightChild);
            }
        }

        // Agacın inOrder Dolasılması
        public void inOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }

        // Agacın postOrder Dolasılması
        public void postOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                postOrder(localRoot.leftChild);
                postOrder(localRoot.rightChild);
                localRoot.displayNode();
            }
        }

        // Agaca bir dügüm eklemeyi saglayan metot
        public void insert(Durak durak)
        {
            TreeNode newNode = new TreeNode();
            newNode.durak = durak;
            if (root == null)
                root = newNode;
            else
            {
                TreeNode current = root;
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    if (current.durak.DurakAdi.Replace("İ", "I").CompareTo(durak.DurakAdi.Replace("İ", "I")) == 1) ///eklenen durak nesnesinin durak adı geçerli düğümün durak adından alfabetik olarak önceyse sol çocuk olarak ağaca eklenir
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else ///eklenen durak nesnesinin durak adı geçerli düğümün durak adından alfabetik olarak sonraysa sağ çocuk olarak ağaca eklenir
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;
                        }
                    }
                } // end while
            } // end else not root
        } // end insert()

        //traverse preorder to extract information about depth, element count, and value of nodes
        private void traverseTreeForInfo(TreeNode node, int depth)
        {
            if (node != null)
            {
                depth++;

                elementCountForEachDepth[depth]++;
                sumElementValuesForEachDepth[depth] += node.durak.BosPark;

                if (depth > maxDepth)
                    maxDepth = depth; //update max depth

                totalDepth += depth;

                //for calculating the average leaves depth
                if (node.leftChild == null && node.rightChild == null)
                {
                    leavesCount++;
                    leavesDepthTotal += depth;
                }

                traverseTreeForInfo(node.leftChild, depth); //traverse left sub-tree
                traverseTreeForInfo(node.rightChild, depth); //traverse right sub-tree

            }
        }

        public void findAndWriteTreeInfo(TreeNode rootNode, int treeSize)
        {

            totalDepth = 0;
            maxDepth = 0;

            elementCountForEachDepth = new int[treeSize];
            sumElementValuesForEachDepth = new int[treeSize];

            //For average leaves depth
            leavesDepthTotal = 0;
            leavesCount = 0;

            traverseTreeForInfo(rootNode, -1);

            Console.WriteLine("\nAğacın derinliği: " + maxDepth);
            Console.WriteLine("Her bir düzeydeki eleman sayısı ve boş park sayılarının toplamı");
            for (int i = 0; i <= maxDepth; i++)
            {
                Console.WriteLine("\tDüzey {0}: Eleman Sayısı : {1},  Boş Park Sayılarının Toplamı : {2}",
                    i, elementCountForEachDepth[i], sumElementValuesForEachDepth[i]);
            }
            Console.WriteLine("Yaprakların ortalam derinliği : " + ((double)leavesDepthTotal / leavesCount));
        }

        public void displayTree(int[] array, int currentSize)
        {
            int nBlanks = 32;
            int itemsPerRow = 1;
            int column = 0;
            int j = 0; // current item
            String dots = "...............................";
            Console.WriteLine(dots + dots); // dotted top line
            while (currentSize > 0)
            {
                if (column == 0) // first item in row?
                    for (int k = 0; k < nBlanks; k++) // preceding blanks
                        Console.Write(" ");
                // display item
                Console.Write(array[j]);
                if (++j == currentSize) // done?
                    break;

                if (++column == itemsPerRow) // end of row?
                {
                    nBlanks /= 2; // half the blanks
                    itemsPerRow *= 2; // twice the items
                    column = 0; // start over on
                    Console.WriteLine(""); // new row
                }
                else // next item on row
                    for (int k = 0; k < nBlanks * 2 - 2; k++)
                        Console.Write(" "); // interim blanks
            }
            Console.WriteLine("\n" + dots + dots); // dotted bottom line
        }
    } // class Tree
}