:- encoding(utf8).

diff([], _, []).

diff([H|T], Set2, Result) :-
    member(H, Set2),
    diff(T, Set2, Result).

diff([H|T], Set2, [H|Result]) :-
    \+ member(H, Set2),
    diff(T, Set2, Result).

go :-
    write('Введите первое множество ([...,...,...].): '),
    read(SetA),

    write('Введите второе множество ([...,...,...].): '),
    read(SetB),
 
    diff(SetA, SetB, Result),
  
    write('Разность (A \\ B): '), write(Result), nl.

:- go.