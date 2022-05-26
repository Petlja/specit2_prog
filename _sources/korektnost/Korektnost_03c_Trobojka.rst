Пример: тробојка
================

**Задатак:** Написати програм који учитава низ целих бројева а затим
га трансформише тако да елементи буду подељени у три дела у зависности
од задатих вредности :math:`A` и :math:`B`. У првом делу су елементи
мањи од задате вредности :math:`А` (вредности из интервала
:math:`[-\infty, A)`), у другом елементи већи или једнаки задатој
вредности :math:`А` и мањи или једнаки задатој вредности :math:`В`
(вредности из интервала :math:`[A, B]`), а у трећем елементи већи од
задате вредности :math:`В` (вредности из интервала :math:`(B,
+\infty)`). Није битно у ком се редоследу налазе елементи унутар
делова. Учитати елементе у низ, а затим реорганизовати редослед
елемената у том низу (не користити помоћне низове).

Улаз
----

У једној линији стандардног улаза налази се број елемената низа,
:math:`N`, а затим се, у наредној линији налазе елементи низа раздвојени
размацима. У последње две линије се налазе цели бројеви :math:`А` и
:math:`В` одвојени празнином, и при томе је :math:`A < B`.

Излаз
-----

Исписати елементе резултујућег низа на стандардни излаз (могуће је
исписати елементе сваке од три групе у посебном реду, раздвојене
размацима, а могуће је исписати и цео низ у једном реду или у више
редова).

Пример
------

Улаз
~~~~

::

   10
   1 3 5 4 8 5 7 2 3 6
   3
   5


Излаз
~~~~~

::

   1 2
   5 3 5 3
   7 6 8

Алгоритам холандске тробојке
----------------------------

Задатак можемо решити помоћу само једног пролаза кроз низ и то “у месту”
тј. без коришћења помоћног низа. Алгоритам у наставку познат је под
називом “Холандска застава тробојка” (енгл. Dutch national flag) и
приписује се чувеном информатичару Дајкстри (енгл. Edsger W. Dijkstra).

Одржаваћемо три променљиве :math:`l`, :math:`d` и :math:`i` и током
петље наметнућемо да важи :math:`0 \leq l \leq d \leq i \leq n` и да
важе следећи услови.

-  У интервалу позиција :math:`[0, l)` налазиће се елементи мањи од
   :math:`A` тј. бројеви из интервала :math:`(-\infty, A)`,
-  у интервалу позиција :math:`[l, i)` налазиће се елементи из интервала
   :math:`[A, B]`,
-  у интервалу позиција :math:`[i, d)` налазиће се елементи који још
   нису испитани,
-  у интервалу позиција :math:`[d, n)` налазиће се елементи који су већи
   од :math:`B` тј. елементи из интервала :math:`(B, +\infty)`.

Дакле, одржавамо распоред ``<<<<===???>>>``.

Да би инваријанта важила пре уласка у петљу, јасно је да мора да важи да
је :math:`i = 0` и :math:`d=n` (јер су сви елементи из интервала
:math:`[i, d) = [0, n)` неиспитани). Такође, да бисмо били сигурни да су
и интервалу :math:`[0, l)` сви елементи мањи од :math:`A`, тај интервал
мора бити празан и мора да важи да је :math:`l=0`. Након овакве
иницијализације и интервал :math:`[l, i) = [0, 0)` и интервал
:math:`[d, n) = [n, n)` је празан, па задовољава наметнути услов.

Петља ће се извршавати док год има неиспитаних елемената, а то је док је
:math:`i < d`. Размотримо како треба да изгледа тело петље, да би услови
били одржани.

-  Ако је елемент на позицији :math:`i` мањи од броја :math:`A` тада
   ћемо га заменити са елементом на позицији :math:`l` (првим елементом
   из интервала :math:`[A, B]`), након чега можемо увећати и :math:`i` и
   :math:`l`.
-  У супротном, ако је елемент на позицији :math:`i` мањи или једнак од
   :math:`B` он припада интервалу :math:`[A, B]` и већ је на свом
   допуштеном месту, па само можемо увећати вредност :math:`i`.
-  У супротном елемент је већи од :math:`B` и тада можемо смањити
   вредност :math:`d` и разменити елемент на позицији :math:`i` са
   елементом на (умањеној) позицији :math:`d`, не мењајући вредност
   :math:`i` (да би се елемент који је управо доведен на позицију
   :math:`i` могао испитати у наредној итерацији).

На крају петље важи да је :math:`i=d`. Уз остале наметнуте услове
тврђење одатле следи (елементи из интервала позиција :math:`[0, l)` су
мањи од :math:`A`, елементи из интервала позиција
:math:`[l, i) = [l, d)` су између :math:`A` и :math:`B`, интервал
непрегледаних елемената :math:`[i, d)` је празан, док су елементи из
интервала :math:`[d, n)` већи од :math:`B`. Дакле, низ је разбијен на
надовезане сегменте :math:`[0, l)`, :math:`[l, d)` и :math:`[d, n)` и у
сваком сегменту се налазе одговарајући елементи.

.. code:: csharp

   using System;
    
   class Program
   {
       // funkcija ucitava elemente niza sa standadnog ulaza
       static int[] unosNiza()
       {
           int n = int.Parse(Console.ReadLine());
           int[] a = new int[n];
           string[] str = Console.ReadLine().Split();
           for(int i = 0; i < n; i++)
               a[i] = int.Parse(str[i]);
           return a;
       }
    
       // razmena elemenata a[i] i a[j]
       static void zameni(int[] a, int i, int j)
       {
           int pom = a[i];
           a[i] = a[j];
           a[j] = pom;
       }
    
       // funkcija organizuje elemente vektora tako da se prvo nalaze
       // elementi za koje vazi da su iz intervala (-Inf, A), nakon
       // toga dolaze elementi iz intervala [A, B], i nakon toga
       // elementi iz intervala (B, Inf)
       static void podelaNiza(int[] niz, int A, int B)
       {
           // - u intervalu pozicija [0, l) su elementi iz intervala (-Inf, A)
           // - u intervalu pozicija [l, i) su elementi iz intervala [A, B]
           // - u intervalu pozicija [i, d) su jos neispitani elementi
           // - u intervalu pozicija [d, n) su elementi iz intervala (B, Inf)
           int l = 0, i = 0, d = niz.Length;
           // dok god postoje neispitani elementi
           while (i < d)
           {
               if (niz[i] < A)
                   // menjamo tekuci element sa prvim elementom iz
                   // intervala [A, B]
                   zameni(niz, i++, l++);
               else if (niz[i] <= B)
                   // tekuci element ostaje na svom mestu
                   i++;
               else
                   // menjamo tekuci element sa poslednjim neispitanim
                   zameni(niz, i, --d);
           }
       }
    
       // funkcija ispisuje elemente niza na standardni izlaz
       static void ispisNiza(int[] a, int A, int B)
       {
           int i = 0;
           // ispisujemo elemente iz intervala (-Inf, A)
           while (i < a.Length && a[i] < A)
               Console.Write(a[i++] + " ");
           Console.WriteLine();
           // ispisujemo elemente iz intervala [A, B]
           while (i < a.Length && a[i] <= B)
               Console.Write(a[i++] + " ");
           Console.WriteLine();
           // ispisujemo elemente iz intervala (B, +Inf)
           while (i < a.Length)
               Console.Write(a[i++] + " ");
           Console.WriteLine();
       }
    
       static void Main(string[] args)
       {
           // ucitavamo elemente niza
           int[] a = unosNiza();
           // ucitavamo interval [A, B]
           int A = int.Parse(Console.ReadLine());
           int B = int.Parse(Console.ReadLine());
               
           // reorganizujemo elemente po intervalima (-inf, A),
           // [A, B] i [B, inf)
           podelaNiza(a, A, B);
    
           // ispisujemo rezultat
           ispisNiza(a, A, B);
       }
   }

