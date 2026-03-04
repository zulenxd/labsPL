open System

let rec maxcifra n = 
    if n = 0 then
        0
    else 
        max (maxcifra(n/10)) (n%10)

[<EntryPoint>]
let main args = 
    printf "введите число: "
    let input = Console.ReadLine()
    let n = abs (int input)
    let cifra = maxcifra n
    printf "%i" cifra
    0