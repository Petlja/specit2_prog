
..
  Рекурзија - како ради рекурзија - тест
  quiz

Рекурзија - како ради рекурзија (програмски стек) - тест
========================================================

.. fillintheblank:: rekurzija_stek_1
		    
   Како се зове део меморије рачунара у ком се чувају параметри и
   локалне променљиве за сваки позив функције?

   - :^stek|Stek|Стек|стек$: Тачан одговор!
     :.*: Покушај поново.

.. fillintheblank:: rekurzija_stek_2
		    
   Ако једна променљива типа ``int`` заузима 4 бајта, колико меморије
   укупно заузимају параметри свих позива функције ``stepen`` насталих 
   из позива ``stepen(2, 6)`` (бројећи и њега), ако је функција
   степеновања дефинисана на следећи начин?

   .. code-block:: csharp

      static int stepen(int x, int n)
      {
         if (n == 0)
	    return 1;
	 else
	    return x * stepen(x, n-1);
      }


   - :^56$: Тачан одговор!
     :.*: Покушај поново.

.. mchoice:: rekurzija_stek_3
   :multiple_answers: 	     
   :answer_a: функција је испрограмирана без рекурзије, а мора бити испрограмирана рекурзивно
   :answer_b: недостаје излаз из рекурзије
   :answer_c: функција је позвана за превелику вредност улазног параметра
   :answer_d: у програму се јавља бесконачна петља while
   :correct: b, c
   :feedback_a: Не.
   :feedback_b: Тачно!
   :feedback_c: Тачно!
   :feedback_d: Не.
		
   Приликом покретања програма исписана је порука ``Stack overflow
   exception``.  Шта може бити проблем?


.. fillintheblank:: rekurzija_stek_4
		    
   Шта исписује овај програм?

   .. code-block:: csharp

        using System;

        class Program
        {
            static void f(int d)
            {
                if (d > 0)
                {
                    int n = d;
                    f(d - 1);
                    Console.Write(n);
                }
            }

            static void Main(string[] args)
            {
                f(5);
                Console.WriteLine();
            }
        }


   - :^12345$: Тачан одговор!
     :.*: Покушај поново.


.. fillintheblank:: rekurzija_stek_5
		    
   Шта исписује овај програм?

   .. code-block:: csharp

        using System;

        class Program
        {
            static int n;
            static void f(int d)
            {
                if (d > 0)
                {
                    n = d;
                    f(d - 1);
                    Console.Write(n);
                }
            }

            static void Main(string[] args)
            {
                f(5);
                Console.WriteLine();
            }
        }


   - :^11111$: Тачан одговор!
     :.*: Покушај поново.

