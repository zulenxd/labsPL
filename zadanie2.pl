:- encoding(utf8).


count_digits([], _, 0) :- !. 

count_digits([H|T], D, Count) :-
    H mod 10 =:= D, !,                  
    count_digits(T, D, TailCount),      
    Count is TailCount + 1.            
count_digits([_|T], D, Count) :-
    count_digits(T, D, Count).          

go :-
    write('Введите список натуральных чисел через [].: '),
    read(List),
    write('Введите искомую последнюю цифру: '),
    read(Digit),
    ( is_list(List) ->
        count_digits(List, Digit, Result),
        write('Количество чисел, оканчивающихся на '), write(Digit), write(': '), write(Result), nl
    ; write('Ошибка: проверьте правильность ввода списка.'), nl
    ).

:- go.