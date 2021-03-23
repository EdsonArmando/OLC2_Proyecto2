program matrices;
type
    matriz = array [0..4,0..4] of integer;
	const min : integer = 0;
	const max : integer = 4;
	var	matrixR : array [0..4, 0..4] of integer;
	var	matrixA : array [0..4,0..4] of integer;
	var matrixB : array [0..4,0..4] of integer;
procedure llenado(var matrixA : matriz ; matrixB : matriz);
var	i,j : integer;
begin
	for i := min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			matrixA[i,j] := j * 3 + i;
			matrixB[i,j] := (i*i*i) - j * j;
		end;
	end;
end;
procedure printMatrix(var matrix : matriz);
var
	i,j : integer;
begin
	for i := min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			write('    |    ',matrix[i,j]);			
		end;
		writeln('    |');
	end;
end;
procedure suma(var matrixA : matriz ; var matrixB : matriz ; var matrixR : matriz);
var	i,j : integer;
begin
	for i:= min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			matrixR[i,j] := matrixA[i,j] + matrixB[i,j];
		end;
	end;
end;
procedure sumaFilas(var matrix : matriz);
var
	i,j,aux : integer;
begin
	writeln('                                        R');
	for i := min to max - 1 do
	begin
		aux := 0;
		for j := min to max - 1 do
		begin
			aux := aux + matrix[i,j];
			write('    |    ',matrix[i,j]);
		end;
		writeln('    |    ',aux);
	end;
end;
procedure sumaColumnas(var matrix : matriz);
var
	i,j,aux : integer;
begin
	write('R');
	for i := min to max - 1 do
	begin
		aux := 0;
		for j := min to max - 1 do
		begin
			aux := aux + matrix[j,i];
		end;
		write('    |    ',aux);
	end;
	writeln('');
end;
procedure resta(var matrixA : matriz ; var matrixB : matriz ; var matrixR : matriz);
var
	i,j : integer;
begin
	for i:= min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			matrixR[i,j] := matrixA[i,j] - matrixB[i,j];
		end;
	end;
end;
procedure Mult(var matrixA : matriz; var matrixB : matriz ; var matrixR : matriz);
var
	i,j,k : integer;
begin
	for i:= min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			for k := 0 to max - 1 do
			begin
				matrixR[i,j] := matrixR[i,j] + matrixA[i,k] * matrixB[k,j];
			end;
		end;
	end;
end;
procedure Transpose(var matrix : matriz);
var	matrixAux : array[0..4,0..4] of integer;
var	i,j : integer;
begin
	for i := min to max - 1 do
	begin
		for j:= min to max - 1 do
		begin
			matrixAux[i,j] := matrix[j,i];
		end;
	end;

	for i := min to max - 1 do
	begin
		for j:= min to max - 1 do
		begin
			matrix[i,j] := matrixAux[i,j];
		end;
	end;
end;
procedure minValue(var matrix : matriz);
var
	i,j,iaux,jaux,temp : integer;
begin
	iaux := min;
	jaux := min;
	temp := matrix[min,min];
	for i := min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			if matrix[i,j] < temp then
			begin
				temp := matrix[i,j];
				iaux := i;
				jaux := j;
			end;
		end;
	end;	
	writeln('Min -> [',iaux,',',jaux,'] = ',temp);
end;
procedure maxValue(var matrix : matriz);
var	i,j,iaux,jaux,temp : integer;
begin
	iaux := min;
	jaux := min;
	temp := matrix[min,min];
	for i := min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			if matrix[i,j] > temp then
			begin
				temp := matrix[i,j];
				iaux := i;
				jaux := j;
			end;
		end;	
	end;	
	writeln('Max -> [',iaux,',',jaux,'] = ',temp);
end;
procedure sort(var matrix : matriz);
var	i,j,x,y,aux : integer;
begin
	for i := min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			for x := 0 to i do
			begin
				for y := 0 to j do
				begin
					if matrix[i,j] < matrix[x,y] then
					begin
						aux := matrix[i,j];
						matrix[i,j] := matrix[x,y];
						matrix[x,y] := aux;
					end;
				end;
			end;
		end;	
	end;		
end;
procedure clearMat(var matrix : matriz);
var
	i,j : integer;
begin
	for i := min to max - 1 do
	begin
		for j := min to max - 1 do
		begin
			matrix[i,j] := 0;
		end;
	end;
end;
llenado(matrixA,matrixB);
writeln('Matrix A');
printMatrix(matrixA);
writeln('Matrix B');
printMatrix(matrixB);
writeln('MatR = MatA + MatB');
suma(matrixA,matrixB,matrixR);
printMatrix(MatrixR);
writeln('MatR = MatA - MatB');
resta(matrixA,matrixB,matrixR);
printMatrix(MatrixR);
writeln('Clear MatR');
clearMat(matrixR);
printMatrix(matrixR);
writeln('MatR = MatA * MatB');
mult(matrixa,matrixb,matrixr);
printmatrix(matrixr);

writeln('Tranpose(MatA)');
transpose(matrixA);
printmatrix(matrixa);

minValue(matrixR);
maxValue(matrixR);

writeln('Sort MatA');
sort(matrixR);
printmatrix(matrixR);

minValue(matrixR);
maxValue(matrixR);

writeln('Suma Filas y Columnas');
sumaFilas(matrixa);
sumacolumnas(matrixa);