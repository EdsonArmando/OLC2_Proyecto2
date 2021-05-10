program anidadas;
var	w : integer = 1;
var	x : integer = 2;
var	y : integer = 3;
var	z : integer = 4;
  procedure p1(a : integer; var b : integer);
    var w : integer = 11;
    var x : integer = 12;
	var	y : integer = 13;
	procedure p11();
		var	w : integer = 21;
		var	x : integer = 22;
		procedure p111();
    		var	w : integer = 31;
			begin
				writeln('Local 31 = ',w);
				writeln('Ambito Padre 22 = ',x);
				writeln('Ambito Padre de Padre 13 = ',y);
				writeln('Global 4 = ',z);
				writeln('Parametro por valor de Padre de Padre 1 = ');
				writeln('Parametro por referencia de Padre de Padre 2 = ');			
			end;
    	begin			
        end;
  begin    
  end;
  procedure p11();
  begin
	writeln('Aqui no debe entrar');
  end;
writeln('Valor Antes 2 = ',x);
p1(1,x);
writeln('Valor Despues 1000 = ',x);