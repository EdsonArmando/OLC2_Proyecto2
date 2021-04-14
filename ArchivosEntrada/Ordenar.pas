program Ordenar;
const maximo : Integer = 100; 
const maxVal : Integer= 3000;
var datos: array[1..maximo] of integer;      
var i: integer;       
procedure swap(var a : integer; var b: integer); 
var 
	tmp: integer; 
begin 
	tmp := a; 
   	a := b; 
   	b := tmp; 
end;
procedure generaNumeros();               { Genera números aleatorios } 
begin 
	writeln(''); 
   	writeln('Generando números...'); 
   	for i := 1 to maximo do 
   	begin
    	datos[i] := maximo - i * i ; 
    end;
end;
procedure muestraNumeros();              { Muestra los núms almacenados } 
begin 
	writeln(''); 
   	writeln('Los números son...'); 
   	for i := 1 to maximo do 
   	begin
    	write(datos[i], ' '); 
   	end;
end; 
procedure Burbuja();                     { Ordena según burbuja } 
var 
	cambiado: boolean; 
var temp : Integer;
begin 
	writeln(''); 
   	writeln('Ordenando mediante burbuja...'); 
   	repeat 
     	cambiado := false;    
             								{ No cambia nada aún } 
     	for i := maximo downto 2 do   
     	begin						     	{ De final a principio } 
       		if (datos[i]) < (datos[i-1]) then   { Si está colocado al revés } 
         	begin 
         		temp := datos[i];
         		datos[i] := datos[i-1];			{ Le da la vuelta } 
         		datos[i-1] := temp;							
         		cambiado := true;              { Y habrá que seguir mirando } 
         	end; 
        end;
	until (not (cambiado));                  { Hasta q nada se haya cambiado } 
 end;
 procedure Sort(var l : Integer;var  r: Integer);         { Esta es la parte recursiva } 
var i, j, x, y, temp: integer; 
begin 
	i := l; j := r;                      { Límites por los lados } 
   	x := datos[((l+r) DIV 2)];             { Centro de la comparaciones } 
   	repeat 
     	while ((datos[i]) < x) do i := i + 1;;  { Salta los ya colocados } 
     	while (x < (datos[j])) do j := j - 1;;  {   en ambos lados } 
     
	 	if (i <= j) then                     { Si queda alguno sin colocar } 
       	begin 
       		temp := datos[i];
     		datos[i] := datos[j];			{ Le da la vuelta } 
     		datos[j] := temp;		 	{ Los cambia de lado } 
       		i := i + 1; 
			j := j - 1;          		{ Y sigue acercándose al centro } 
       	end; 
   until (i > j);                         { Hasta que lo pasemos } 
   
   if (l < j) then begin Sort(l, j);end;          { Llamadas recursivas por cada } 
   if (i < r) then begin Sort(i, r);end;            {   lado } 
 end; 
 procedure QuickSort();                   { Ordena según Quicksort } 
begin
	writeln('');
	writeln('Ordenando mediante QuickSort...'); 
	Sort(1,Maximo);
end;
generaNumeros(); 
muestraNumeros(); 
Burbuja();
muestraNumeros(); 
generaNumeros(); 
muestraNumeros(); 
QuickSort(); 
muestraNumeros(); 
