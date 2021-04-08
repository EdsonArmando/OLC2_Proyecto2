program ListaDoble;
type
Points = object
var right : integer;
var left : integer;
var temp : Points;
end;
var dos : Points;
dos.left := 100;
dos.temp.left := 30;
var temporal : Points;


program dos;
var i : Integer = 6;
repeat        
        writeln(i);
        i := i - 2;
    until (i = 0);