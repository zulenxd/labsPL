program mytest;
var a, b: integer;
    f: real;
    arr: array [1..10] of integer;

procedure TestProc(var p1: integer; p2: real);
begin
    p1 := 100;
    p2 := 3.14;
end;

begin
    a := 5;
    arr[1] := 10;
    arr[2] := arr[1] * 2 + a;
    TestProc(arr[2], f);

    b := 5.5;
    a[1] := 5;
    arr[3.5] := 10;
end.
