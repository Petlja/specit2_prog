Проналажење оптималне вредности решења бинарном претрагом
=========================================================

Бинарна претрага се може употребити и у процесу оптимизације, ако се
проблем може формулисати као проблем проналажења преломне тачке. Овај
облик претраге се понекад назива Бинарна претрага по решењу. Идеја је
да се проблем оптимизације “наћи најмању вредност која задовољава
одређени услов”, сведе на проблем одлучивања “да ли дата вредност
задовољава одређени услов”. Бинарну претрагу је могуће применити ако
проблем задовољава својство монотоности, које захтева да ако нека
вредност задовољава услов, онда услов задовољавају и све вредности
веће од ње, а ако не задовољава, онда услов не задовољавају ни
вредности мање од ње. Наравно, сасвим слично може да се пронађе и највећа
вредност која не задовољава услов. Карактеристично за ову употребу
бинарне претраге је то што потенцијалне вредности обично нису смештене
у низ, а често се врши оптимизација и над непрекидним скупом вредности
(до на одређену тачност). Стога се уместо коришћења библиотечких
функција, бинарна претрага ручно имплементира.

Покушај да решиш оптимизационе задатке са следеће странице коришћењем
бинарне претраге. У сваком задатку је кључно да дефинишеш функцију
која проверава да ли тренутна вредност задовољава услове задатка. Та
функција треба по правилу да ради у сложености :math:`O(n)`.

.. comment

    - Хиршов h-индекс
    - Дрва
    - Највећи квадрат у хистограму
    - Муцајући подниз
    - Пуно фигурица
    - Конференција
    - Најкраћа подниска која садржи све дате карактере
    - Кувар
    - Градња
    - Гласници
