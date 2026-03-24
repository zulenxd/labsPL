:- encoding(utf8).

nod(X, 0, X) :- !. 

nod(X, Y, Result) :-
    Y > 0, 
    Remainder is X mod Y,
    nod(Y, Remainder, Result).

main :-
    write('Введите первое натуральное число: '),
    read(Num1),
    write('Введите второе натуральное число: '),
    read(Num2),
    ( Num1 > 0, Num2 > 0 ->
        nod(Num1, Num2, Result),
        write('Наибольший общий делитель: '), write(Result), nl
    ; write('Ошибка: числа должны быть натуральными (положительными).'), nl
    ).

:- initialization(main).