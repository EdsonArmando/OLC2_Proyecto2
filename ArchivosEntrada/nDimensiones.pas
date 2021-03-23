program nDimensiones;
(*Matrices de 3 dimensiones*)
type 
 	reporte = object 
	Var rechazos,dat : INTEGER;
	Var vino,revision,botella : STRING;
end;
Var i,j,k : INTEGER;
var vinos: array[0..4] of String;
var botellas: array[0..3] of String;
var revisiones: array[0..1] of String;
var encabezado: array[0..3] of String;
encabezado [0]:= 'Revision';
encabezado [1]:= 'Tipo Vino';
encabezado [2]:= 'Botella';
encabezado [3]:= 'Total';
var reportes: array[0..1,0..4,0..3] of Reporte;
procedure llenarArreglo();
begin
    vinos[0] := 'Blanco'; 
    vinos[1] := 'Tinto'; 
    vinos[2] := 'Jerez'; 
    vinos[3] := 'Oporto'; 
    vinos[4] := 'Rosado';
    botellas[0] := 'Magnum'; 
    botellas[1] := 'DMagnum'; 
    botellas[2] := 'Estandar'; 
    botellas[3] := 'Imperial';
    revisiones[0] := 'Oxigeno'; 
    revisiones[1] := 'Envases';
end;
function InsertarStruct(var arreglo:Reporte): Reporte;
var valStruct : Reporte;
begin
  for i:=0 to 1 do
	Begin
		for j:=0 to 4 do
		Begin
			for k:=0 to 3 do
			Begin			
				valStruct.vino := vinos[j];
				valStruct.revision := revisiones[i];
				valStruct.botella := botellas[k];
				valStruct.rechazos := i * 5 * 4 + j * 4 + k + 1;
				arreglo [i,j,k]:= valStruct;

				//writeln('arreglo','[' + i + ']','[' + j + ']','[' + k + ']');
			End;
		End;		
	End;
	exit(arreglo);
end;
procedure ImprimirStruct(var repo:Reporte);
begin
  writeln(repo.revision, '        ',repo.vino ,  '         ', repo.botella, '         ', repo.rechazos );
end;
procedure Imprimir(var arreglo:Reporte);
begin
  for i:=0 to 1 do
	Begin
		for j:=0 to 4 do
		Begin
			for k:=0 to 3 do
			Begin			
				ImprimirStruct(arreglo[i,j,k]);				
			End;
		End;		
	End;
end;
procedure ImprimirEncabezado();
begin
  writeln(encabezado[0] + '        ' , encabezado[1] + '         ' ,encabezado[2] + '         '  , encabezado[3]);
end;
llenarArreglo();
InsertarStruct(reportes);
ImprimirEncabezado();
Imprimir(reportes);