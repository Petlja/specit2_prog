Бинарна претрага елемента у низу
================================

Алгоритам бинарне претраге вредности у низу одговара оном који се може
применити у игри погађања непознатог броја и који је заснован на
половљењу интервала. Основна идеја је да се тражени елемент пореди са
средишњим елементом у низу. Ако је тражени елемент мањи од средишњег,
пошто је низ сортиран, знаћемо да је мањи и од свих елемената десно од
тог средишњег, па тај део низа можемо елиминисати (можемо начинити
одсецање у претрази) и претрагу можемо наставити само у левој половини
низа. Симетрично, ако је тражени елемент већи од средишњег, због
сортираности је већи и од свих елемената лево од средишњег и лева
половина низа може бити елиминисана из даље претраге. На крају, ако
елемент није ни мањи ни већи од средишњег, онда му је једнак и
пронађен је у низу. Приметимо да у основи алгоритма бинарне претраге
лежи техника одсецања, која је оправдана тиме што је низ сортиран.

Пошто се у сваком кораку претраге дужина низа дупло смањује, за претрагу
целог низа довољно је :math:`O(\log{n})` корака, где је :math:`n` дужина
низа. Наиме, претрага у најгорем случају траје све док се половљењем низ
не испразни. Дужина низа након :math:`k` корака половљења је отприлике
:math:`\frac{n}{2^k}`. Низ ће се испразнити када је
:math:`\frac{n}{2^k} < 1`, тј. када је :math:`n < 2^k`, тј. када је
:math:`k > \log_2{n}`.

Слично као и за сортирање, већина програмских језика пружа готове
библиотечке функције за бинарну претрагу.

У језику C# функција ``Array.BinarySearch`` врши бинарну претрагу
сортираног низа. Функцији се задаје обавезно низ и вредност која се
тражи. Функција враћа било коју позицију на којој је тражена вредност
пронађена или негативан број који је битовска негација позиције првог
елемента строго већег од тражене вредности (тј. дужине низа ако такав
елемент не постоји). Зато проверу да ли низ ``a`` садржи елемент ``x``
можемо извршити помоћу услова ``Array.BinarySearch(a, x) >= 0``. Ако
је потребно претражити само део низа, тада је могуће као додатне
параметре проследити индекс почетка и број елемената дела низа који се
претражује.

Ако се претражује сортирана листа, користи се метода ``BinarySearch``,
која се понаша скоро потпуно исто као ``Array.BinarySearch`` (једино што
се листа не наводи као аргумент, већ се метода позива на листи). На
пример, провера да ли сортирана листа ``a`` садржи елемент ``x`` може се
извршити позивом ``a.BinarySearch(x) >= 0``.

Ако се не зада другачије, подразумева се да је низ сортиран у односу на
подразумевани поредак елемената (неопадајући нумерички ако су бројеви у
питању, тј. неопадајући абецедни лексикографски ако су ниске у питању).
Поредак се може задати или променити на сличан начин као код функција за
сортирање (о чему ће бити више речи касније).


На пример, у наредном програму се ефикасно проверава колико је датих
елемената садржано у претходно учитаном низу бројева.

.. code:: csharp
          
   using System;
    
   class Program
   {
       static void Main()
       {
           // ucitavamo niz
           int n = int.Parse(Console.ReadLine());
           string[] str = Console.ReadLine().Split();
           int[] a = new int[n];
           for (int i = 0; i < n; i++)
               a[i] = int.Parse(str[i]);
           // broj onih koji postoje u nizu
           int broj = 0;
           // ucitavamo broj po broj do kraja ulaza
           string s;
           while ((s = Console.ReadLine()) != null) {
               int x = int.Parse(s);
               // ako je broj sadrzan u nizu, uvecavamo brojac
               if (Array.BinarySearch(a, x) >= 0)
                   broj++;
           }
           // ispisujemo rezultat
           Console.WriteLine(broj);
       }
   }

Бинарна претрага елемента у низу се једноставно може имплементирати и
ручно.

.. code:: csharp

   using System;
    
   class Program
   {
       // funkcija proverava da li se u datom sortiranom nizu a
       // duzine n nalazi element x
       static bool sadrzi(int[] a, int x)
       {
           // petrazujemo da li se element nalazi u intervalu [l, d]
           int l = 0, d = a.Length - 1;
           // dok god taj interval nije prazan
           while (l <= d) {
               // nalazimo sredinu intervala
               int s = l + (d - l) / 2;
               // ako je x manji od srednjeg on se moze nalaziti samo u intevalu
               // [a, s-1] (jer je niz sortiran)
               if (x < a[s])
                   d = s - 1;
               // ako je x veci od srednjeg on se moze nalaziti samo u intevalu
               // [s+1, d] (jer je niz sortiran)
               else if (x > a[s])
                   l = s + 1;
               else
                   // nasli smo element x na poziciji s
                   return true;
           }
           // element ne postoji u nizu
           return false;
       }
       
       static void Main()
       {
           // ucitavamo niz
           int n = int.Parse(Console.ReadLine());
           int[] a = new int[n];
           string[] str = Console.ReadLine().Split();
           for (int i = 0; i < n; i++)
               a[i] = int.Parse(str[i]);
           // broj onih koji postoje u nizu
           int broj = 0;
           // ucitavamo broj po broj do kraja ulaza
           string s;
           while ((s = Console.ReadLine()) != null) {
               int x = int.Parse(s);
               // ako je broj sadrzan u nizu, uvecavamo brojac
               if (sadrzi(a, x))
                   broj++;
           }
           // ispisujemo rezultat
           Console.WriteLine(broj);
       }
   }
          
   
Покушај сада да техником бинарне претраге ефикасно решиш задатке
на следећој страни.

.. comment 
      
    - Провера бар-кодова
    - Број парова датог збира
    - Квадрати
    - Ранг сваког елемента
    - i-ти на месту i
