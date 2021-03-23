program Funciones;
function FactoriaL(numero:integer):Integer;
 Var q : integer = 100;
    begin
        if (numero = 0) then
            begin
                exit(1);
            end;
        else begin
           exit(numero * FactoriaL(numero-1));
        end;
    end;

function ackermann(var m:integer ; var n:integer): Integer;
    begin
        if (m=0) then
        begin
            exit(n+1);
        end;
        else if((m>0) and (n=0)) then
        begin
            exit(ackermann(m-1,1));
        end;
        else begin
            exit(ackermann(m-1,ackermann(m,n-1)));
        end;
    end;
procedure Hanoi(var discos:integer; var origen: String;var aux : String;var destino:string);
begin
if(discos=1) then
begin
    writeln('Mover Disco de ',origen,' a ',destino);
end;
else
    Begin
    Hanoi(discos-1,origen,destino,aux);
    writeln('Mover disco de ',origen,' a ',destino);
    Hanoi(discos-1,aux,origen,destino);
    End;
end;
writeln('1 Factorial');
writeln(factorial(6));
writeln('2 Ackermann');
writeln(ackermann(3,4));
writeln('3 Hanoi');
Hanoi(3, 'A', 'B', 'C');