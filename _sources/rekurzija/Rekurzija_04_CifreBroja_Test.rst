Рекурзија - цифре у запису броја - тест
=======================================

.. fillintheblank:: rekurzija_cifre_1
                    
   Шта исписује наредни програм (пробај да одговориш без његовог покретања)?

   .. code-block:: csharp

      static int f(int n)
      {
          if (n == 0)
             return 0;
          return f(n / 10) + (n % 10) * (n % 10);
      }

      static void Main()
      {
         Console.WriteLine(f(567));
      }
   

   - :^110$: Тачан одговор!
     :.*: Покушај поново.

.. fillintheblank:: rekurzija_cifre_2
                    
   Шта исписује наредни програм (пробај да одговориш без његовог покретања)?

   .. code-block:: csharp

      static int f(int n)
      {
          if (n < 2)
             return n == 0 ? 1 : 0;
          return f(n / 2) + (n % 2 == 0 ? 1 : 0);
      }

      static void Main()
      {
         Console.WriteLine(f(25));
      }
   

   - :^2$: Тачан одговор!
     :.*: Покушај поново.

.. fillintheblank:: rekurzija_cifre_3
                    
   Шта исписује наредни програм (пробај да одговориш без његовог покретања)?

   .. code-block:: csharp

      static void f(int n)
      {
          if (n < 8)
             Console.Write(n);
          else
          {
             Console.Write(n % 8);
             f(n / 8);
          }
      }

      static void Main()
      {
         f(83);
      }
   

   - :^321$: Тачан одговор!
     :.*: Покушај поново.
        
.. mchoice:: rekurzija_cifre_4
   :answer_a: збир квадрата цифара броја
   :answer_b: збир квадрата парних цифара броја
   :answer_c: збир квадрата непарних цифара броја
   :answer_d: збир цифара непарног броја
   :correct: c
   :feedback_a: Не.
   :feedback_b: Не.
   :feedback_c: Тачно!
   :feedback_d: Не.
                
   Шта израчунава ова функција?
   
   .. code-block:: csharp

      static void f(int n)
      {
          if (n == 0)
             return 0;
          else
          {
             if (n % 2 == 1)
                 return f(n / 10) + (n % 10) * (n % 10);
             else
                 return f(n / 10);
          }
      }

