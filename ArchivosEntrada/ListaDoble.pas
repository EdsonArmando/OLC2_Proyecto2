program ListaDoble;
type
Points = object
var right : integer;
var left : integer;
end;
type
Node = object
var idx : integer;
var val : integer;
var point : Points;
end;
type
DoubleList = array[1..20] of Node;
var actualDL : DoubleList;
var count : integer = 1;
var first : integer = -1;
var last : integer = -1;
var i : integer = 0;

procedure PrintListback();
var actual : Node;
var i : integer;
begin
    if ((first) <> (-1)) then
    begin
        i := last;
        repeat
            actual := actualDL[i];
            writeln('Valor de nodo: ', actual.val);
            i := actual.point.left;
        until ((actual.idx) = (first));
    end;
end;