Сортирање по разним критеријумима
=================================

Сортирање подразумева да се елементи ређају у односу на неки поредак.
Бројеви се подразумевано ређају по њиховој нумеричкој вредности, ниске
се подразумевано ређају лексикографски абецедно (као у речнику), парови
и торке се поново ређају лексикографски, по својим компонентама,
међутим, често је потребно сортирати низове елемената над којима није
дефинисан подразумевани поредак (на пример, низове структура).
Библиотечке функције сортирања допуштају навођење различитих критеријума
сортирања, навођењем додатних аргумената.

Сортирање подразумева да се елементи ређају у односу на неки поредак.
Бројеви се подразумевано ређају по њиховој нумеричкој вредности, ниске
се подразумевано ређају лексикографски абецедно (као у речнику), парови
и торке се поново ређају лексикографски, по својим компонентама,
међутим, често је потребно сортирати низове елемената над којима није
дефинисан подразумевани поредак (на пример, низове структура).
Библиотечке функције сортирања допуштају навођење различитих критеријума
сортирања, навођењем додатних аргумената.

Када се сортира низ структура или објеката, критеријум је могуће задати
тако што се у самој структури тј. класи дефинише метода која врши
поређење. У језику C# потребно је имплементирати интерфејс
``IComparable`` и дефинисати методу ``CompareTo``. Функција треба да
врати негативан број, нулу или позитиван број у односу на то да ли је
објекат на ком је позвана мањи, једнак или већи од објекта који јој је
прослеђен као аргумент (у сортираном редоследу мањи објекти иду испред
већих).

Приликом сортирања било ког типа елемената, функцији за сортирање могуће
је као додатни параметар проследити функцију поређења (било анонимну,
било именовану). Она прима два објекта која треба да упореди и враћа
негативан број, нулу или позитиван број у односу на то да ли јој је први
прослеђени аргумент мањи, једнак или већи од другог прослеђеног
аргумента (у сортираном редоследу мањи објекти иду испред већих).

Размотримо наредни пример.

Пример: сортирање такмичара
---------------------------

Дат је низ такмичара, за сваког такмичара познато је његово име и број
поена на такмичењу. Написати програм којим се сортира низ ученика
нерастуће по броју поена, а ако два такмичара имају исти број поена,
онда их уредити по имену у нерастућем поретку.

Улаз
----

У првој линији стандардног улаза налази се природан број :math:`n`
(:math:`n \leq 50000`). У следећих :math:`n` линија налазе се редом
елементи низа. За сваког такмичара, у једној линији, налази се
одвојени једним бланко симболом, његово име (дужине највише 20
карактера) и број поена (природан број из интервала :math:`[0,10000]`)
које је такмичар освојио.

Излаз
-----

На стандардни излаз исписати елементе уређеног низа такмичара, за сваког
такмичара у једној линији приказати његово име и број поена, одвојени
једним бланко симболом.

Пример
------

Улаз
~~~~

::

   5
   Maja 56
   Marko 78
   Krsto 23
   Jovan 78
   Milica 89

Излаз
~~~~~

::

   Milica 89
   Jovan 78
   Marko 78
   Maja 56
   Krsto 23

Опис решења
-----------

Прво питање које је потребно разрешити је како чувати податке о
такмичарима. Најприродније решење да се подаци о такмичару памте у
структури ``Takmicar`` чији су елементи име такмичара (податак типа
``string``) и број поена (податак типа ``int``). За чување информација о
свим такмичарима можемо онда употребити неку колекцију структура. Друге
могућности су да се подаци чувају у два низа (низу имена и низу поена)
или да се уместо структура користе парови тј. торке.

Сортирање је могуће извршити на разне начине.

Сортирање селекцијом
~~~~~~~~~~~~~~~~~~~~

Један од начина је да се ручно имплементира неки алгоритам сортирања
(као пример, наводимо најједноставнију имплементацију алгоритма
SelectionSort), међутим то је обично веома компликовано и неефикасно.


.. code:: csharp

   using System;
    
   class Program
   {
       struct Takmicar
       {
           public string ime;
           public int brojPoena;
       };
       
       static bool uporedi(Takmicar A, Takmicar B)
       {
           if (A.brojPoena > B.brojPoena)
               return true;
           if (A.brojPoena < B.brojPoena)
               return false;
           return String.Compare(A.ime, B.ime) <= 0;
       }
       
       static void razmeni(ref Takmicar A, ref Takmicar B)
       {
           Takmicar P = A;
           A = B;
           B = P;
       }
       
       static void Main()
       {
           int n = int.Parse(Console.ReadLine());
           Takmicar[] a = new Takmicar[n];
           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               a[i].ime = r[0];
               a[i].brojPoena = int.Parse(r[1]);
           }
           
           for (int i = 0; i < n - 1; i++)
           {
               int iMax = i;
               for (int j = i + 1; j < n; j++)
                   if (uporedi(a[j], a[iMax]))
                       iMax = j;
               razmeni(ref a[iMax], ref a[i]);
           }
           
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].ime + " " + a[i].brojPoena);
       }
   }


Библиотечка функција сортирања
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

Најбољи и уједно и најједноставнији начин је употребити библиотечку
функцију сортирања. У језику C# то је функција ``Array.Sort`` ако се
користи низ тј. методу ``List.Sort`` ако се у користи листа.

Функцији сортирања је потребно доставити и функцију поређења којом се
заправо одређује редослед елемената након сортирања. Тело те функције
поређења врши вишекритеријумско (лексикографско) поређење уређених
двојки података. Прво се пореди број поена и ако је број поена првог
такмичара мањи функција поређења враћа ``false`` (јер он треба да иде
иза другог такмичара), ако је већи враћа се ``true`` (јер он треба да
иде испред другог такмичара), а ако су бројеви поена једнаки, прелази се
на поређење имена. За то се може упоредити библиотечко абецедно
(лексикографско) поређење ниски. У језику C# се за то може употребити
било функција ``String.Compare``, било метода ``Compare`` класе
``String``.

Податке о такмичарима можемо сместити у низ типа ``Takmicar[]`` или у
листу типа ``List<Takmicar>``. У првом случају за сортирање користимо
функцију ``Array.Sort`` којој је први параметар низ такмичара, а други
функција поређења, док у другом случају користимо методу ``Sort`` коју
позивамо на листи такмичара и као параметар јој евентуално предајемо
функцију поређења. Начини да се зада функција поређења исти су у оба
случаја.

Функција поређења прима две структуре које садрже податке о два
такмичара који се пореде, али не враћа податак типа ``bool`` већ податак
типа ``int``. Тај број треба да буде негативан ако је први такмичар
испред првог, једнак нули ако су такмичари једанки, а позитиван ако је
први такмичар иза другог у сортираном редоследу.

Именована функција поређења
~~~~~~~~~~~~~~~~~~~~~~~~~~~

Један начин је да се дефинише именована функција поређења и да се њено
име наведе у позиву ``Sort``.

.. code:: cpp

   static int uporedi(Takmicar A, Takmicar B)
   {
     ...
   }

   Array.Sort(a, uporedi);

Комплетно решење се може имплементирати на следећи начин.

.. code:: csharp

   using System;

   class Program
   {
       struct Takmicar
       {
           public string ime;
           public int brojPoena;
       };
       
       static int uporedi(Takmicar A, Takmicar B)
       {
           if (A.brojPoena > B.brojPoena)
               return -1;
           if (A.brojPoena < B.brojPoena)
               return 1;
           return String.Compare(A.ime, B.ime);
       }
       
       static void Main()
       {
           // učitavamo takmičare
           int n = int.Parse(Console.ReadLine());
           Takmicar[] a = new Takmicar[n];
           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               a[i].ime = r[0];
               a[i].brojPoena = int.Parse(r[1]);
           }
           
           // sortiramo takmičare
           Array.Sort(a, uporedi);
           
           // ispisujemo rezultat
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].ime + " " + a[i].brojPoena);
       }
   }

Анонимна функција поређења
~~~~~~~~~~~~~~~~~~~~~~~~~~

Други начин је да се наведе анонимна функција поређења. Она може бити
задата у облику ламбда-израза.

.. code:: cpp

   Array.Sort(a, (A, B) => ... );

Комплетно решење се може имплементирати на следећи начин.

.. code:: csharp

   using System;

   class Program
   {
       struct Takmicar
       {
           public string ime;
           public int brojPoena;
       }
       
       static void Main()
       {
           int n = int.Parse(Console.ReadLine());
           Takmicar[] a = new Takmicar[n];
           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               a[i].ime = r[0];
               a[i].brojPoena = int.Parse(r[1]);
           }
           Array.Sort(a, (x, y) =>
                      x.brojPoena != y.brojPoena ?
                      y.brojPoena.CompareTo(x.brojPoena) :
                      String.Compare(x.ime, y.ime));
           
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].ime + " " + a[i].brojPoena);
       }
   }

Функција може бити задата и у облику делегата.

.. code:: csharp

   Array.Sort(a,
              delegate(Takmicar A, Takmicar B)
              {
                 ...
              });

Комплетно решење се може имплементирати на следећи начин.

.. code:: csharp

   using System;
   using System.Collections.Generic;

   class Program
   {
       
       struct Takmicar
       {
           public string ime;
           public int brojPoena;
       }
       
       static void Main()
       {
           int n = int.Parse(Console.ReadLine());
           List<Takmicar> a = new List<Takmicar>(n);
           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               Takmicar takmicar;
               takmicar.ime = r[0];
               takmicar.brojPoena = int.Parse(r[1]);
               a.Add(takmicar);
           }
           
           a.Sort(delegate(Takmicar A, Takmicar B)
                  {
                      if (A.brojPoena > B.brojPoena)
                          return -1;
                      if (A.brojPoena < B.brojPoena)
                          return 1;
                      return String.Compare(A.ime, B.ime);
                  });
           
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].ime + " " + a[i].brojPoena);
       }
   }

Објекат упоређивач
~~~~~~~~~~~~~~~~~~

Трећи начин је да се направи посебан објекат “упоређивач” који
имплементира интерфејс ``IComparer<Takmicar>`` и који у својој методи
``Compare`` врши поређење два такмичара. Природно је да такав објекат
“упоређивач” буде објекат посебне структуре или класе, а могуће је
додати га и некој већ постојећој структури (на пример, структури
``Takmicar``).

.. code:: csharp

   struct PoredjenjeTakmicara : IComparer<Takmicar>
   {
      public int Compare(Takmicar A, Takmicar B)
      {
         ...
      }
   }

   Array.Sort(a, new PoredjenjeTakmicara());

Комплетно решење се може имплементирати на следећи начин.

.. code:: csharp

   using System;
   using System.Collections.Generic;

   class Program
   {
       struct Takmicar
       {
           public string ime;
           public int brojPoena;
       }
       
       struct PoredjenjeTakmicara : IComparer<Takmicar>
       {
           public int Compare(Takmicar A, Takmicar B)
           {
               if (A.brojPoena > B.brojPoena)
                   return -1;
               if (A.brojPoena < B.brojPoena)
                   return 1;
               return String.Compare(A.ime, B.ime);
           }
       }
       
       static void Main()
       {
           // ucitavamo takmicare
           int n = int.Parse(Console.ReadLine());
           Takmicar[] a = new Takmicar[n];

           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               a[i].ime = r[0];
               a[i].brojPoena = int.Parse(r[1]);
           }

           // sortiramo takmicare
           Array.Sort(a, new PoredjenjeTakmicara());
           
           // ispisujemo rezultat
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].ime + " " + a[i].brojPoena);
       }
   }

Оператор поређења
~~~~~~~~~~~~~~~~~

Четврти начин је да се функција поређења инкорпорира у структуру
``Takmicar``. У том случају та структура треба да имплементира
``IComparable<Takmicar>`` интерфејс тако што ће функцију поређења
имплементирати кроз методу ``CompareTo``, а у позиву ``Sort`` није
потребно наводити параметар који се односи на функцију поређења.

.. code:: csharp

   struct Takmicar : IComparable<Takmicar>
   {
     ...
     public int CompareTo(Takmicar A, Takmicar B)
     {
       ...
     }
   }

   Array.Sort(a);

Комплетно решење се може имплементирати на следећи начин.

.. code:: csharp

   using System;

   class Program
   {
       struct Takmicar : IComparable<Takmicar>
       {
           public string ime;
           public int brojPoena;
           public int CompareTo(Takmicar B)
           {
               if (brojPoena > B.brojPoena)
                   return -1;
               if (brojPoena < B.brojPoena)
                   return 1;
               return String.Compare(ime, B.ime);
           }
       };
       
       static void Main()
       {
           // ucitavamo takmicare
           int n = int.Parse(Console.ReadLine());
           Takmicar[] a = new Takmicar[n];
           
           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               a[i].ime = r[0];
               a[i].brojPoena = int.Parse(r[1]);
           }

           // sortiramo takmicare
           Array.Sort(a);
           
           // ispisujemo rezultat
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].ime + " " + a[i].brojPoena);
       }
   }

Коришћење парова или торки
~~~~~~~~~~~~~~~~~~~~~~~~~~

Рецимо и да се за репрезентацију података могу користити парови тј.
торке. У језику C# торке су представљене типом ``Tuple`` (а парове је
могуће реализовати као двочлане торке). Пошто се парови тј. торке
подразумевано пореде лексикографски (прво прва компонента, а тек ако
је прва компонента једнака, онда друга) и неопадајуће, функцију
поређења није неопходно наводити. Зато је паметно парове организовати
тако да се као прва компонента памти супротан број од броја освојених
поена (да би се добио опадајући редослед броја поена), а као друга име
такмичара.

.. code:: csharp

   using System;

   class Program
   {
       static void Main()
       {
           // ucitavamo takmicare
           int n = int.Parse(Console.ReadLine());
           Tuple<int, String>[] a = new Tuple<int, String>[n];
           for (int i = 0; i < n; i++)
           {
               string[] r = Console.ReadLine().Split(' ');
               a[i] = Tuple.Create(-int.Parse(r[1]), r[0]);
           }
           
           // sortiramo takmicare
           Array.Sort(a);
           
           // ispisujemo rezultat
           for (int i = 0; i < n; i++)
               Console.WriteLine(a[i].Item2 + " " + -a[i].Item1);
       }
   }

Покушај сада да применом библиотечке функције сортирања урадиш 
задатке са следеће стране.

.. comment

    -  Сортирање на основи растојања од О
    -  Највреднији предмети
    -  Сортирање линија
    -  Сортирање по просеку
    -  Ранг сваког елемента
    -  Двобојка
       
