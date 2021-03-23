program OrdenamientoRapido;
type
    v_ = array [0..6] of integer;
var
	v : array[0 .. 6] of integer;

(* paso el arreglo por referencia *)
function quick_sort(var a : v_; var izq : integer; var der : integer):integer;
var	pivote	: integer;
var	i		: integer;		(* i realiza la búsqueda de izquierda a derecha *)
var	d		: integer;		(* d realiza la búsqueda de derecha a izquierda *)
var	aux		: integer;
begin
    pivote := a[izq];
    i := izq;
    d := der;
	(* WHILEs anidados *)
	while (i < d) do
	begin
		(* Whiles de una sentencia. no necesita begind/end *)
		while (a[i] <= pivote) AND (i < d) do i := i+1;;
		WHILE a[D] > pivote DO d := (d-1);;

		(* parentesis innecesarions *)
		if ((((i < d)))) then		(* sino se han cruzado *)
		begin
			aux := a[i];				(* los intercambia *)
			a[i] := a[d];
			a[D] := aux;
		end;
	end;


	A[izq] := A[D];					(* se colocal el pivote en su lugar de forma que tendremos *)
	A[D] := pivote;				(* los menores a la izquierda y los mayores a su derecha *)

	(* IFs sin begin/end *)
	if izq < (d-1) then begin quick_sort(A, izq, d-1); end;
	if (d+1) < der then begin quick_sort(A, d+1, der); end;
 exit(a);
end;
(* paso por valor el arreglo :O *)
procedure imprimirArreglo(var arrayValor : v_); 
var indicearreglolocal : integer;
begin
	indicearreglolocal := 0;
	for indicearreglolocal := 0 to 5 do
	begin
		if (indicearregloLocal < 5) then begin
			write(arrayValor[indicearreglolocal], ', ');
		end;
		else begin
			write(arrayValor[indicearreglolocal]);
		end;
	end;
end;

v[0] := 40;
	v[1] := 21;
	v[2] := 1;
	v[3] := 3;
	v[4] := 12;
	v[5] := 4;
wRiteLn('Antes del sort: ');
imprimirArreglo(v);
quick_sort(v, 0, 5);
wRiteLn('');
wRiteLn('Despues del sort: ');
imprimirArreglo(v);