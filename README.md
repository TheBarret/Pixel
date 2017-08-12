# Pixel [Work in Progress]
Tiny 16bit Virtual Machine

Assembly Program Running

![](http://i.imgur.com/bPnU768.png)

Example of 'Hello, World' in Pixel language

```
;************************************
; Program: 'Hello, World!' Banner
;************************************
clear
jmp [main]

:x		:10
:y		:10
:index		:0
:timer		:0
:string		@">> Hello, World! <<"

:main	if [index] #19 jmp [a1]			;did we reach end of the string?

:loop	print [x][y][index][string]		;print character from string
	load [index]				;increase index
	push #1
	add
	store [index]
	load [x]				;increase x
	push #5
	add
	store [x]
	jmp [main]				;jump back to begin

:a1	storev [timer] #150			;create a time out
:a2	ifg [timer] #0 jmp [a3]
	storev [x] #10				;reset variables
	storev [y] #10
	storev [index] #0
	jmp [loop]				;jump back

:a3	load [timer]				;decrease timer
	push #1
	sub
	store [timer]
	jmp [a2]
```

Instructions
```
Function	      	  | Parameters                    		| Description
--------------------------+---------------------------------------------+---------------------------------------------------
push 		          | (#<num>|0x<hex>|[label])			| Loads value onto the stack
pop		          | no params			                | Pops value from the stack
load		          | ([label])			                | Loads variable onto the stack
store		          | ([label])			                | Stores value from stack to variable
storev		          | ([label]) (#<num>|0x<hex>)	                | Stores value to variable
jmp		          | ([label])			                | Jumps to label location
call		          | ([label])			                | Call procedure
return		      	  | no params			                | Returns from procedure
add		          | no params			                | Performs an addition of the two stack values
sub		          | no params			                | Performs a subtraction of the two stack values
mul		          | no params			                | Performs a multiplication of the two stack values
div		          | no params			                | Performs a division of the two stack values
mod		          | no params			                | Performs a modulus of the two stack values
and			  | no params			                | Performs a bitwise AND of the two stack values
or		          | no params			                | Performs a bitwise OR of the two stack values
xor		          | no params			                | Performs a bitwise XOR of the two stack values
shr		          | no params			                | Performs a bitwise shift right of the two stack values
shl		          | no params			                | Performs a bitwise shift left of the two stack values
addressOf		  | ([label])					| Pushes memory address of label onto the stack
writeAt			  | ([label])					| Writes stack value to memory address
random			  | (#<num>|0x<hex>)				| Pushes random number, starting from 0 and the parameter defines max range
if			  | [label] (#number)				| Performs a condition test x == y, executes next instruction if true
ifn			  | [label] (#number) 				| Performs a condition test x != y, executes next instruction if true
ifg			  | [label] (#number)				| Performs a condition test x >  y, executes next instruction if true
ifl			  | [label] (#number) 				| Performs a condition test x <  y, executes next instruction if true
ifv			  | [label] [label]				| Performs a condition test x == y, executes next instruction if true
ifnv			  | [label] [label] 				| Performs a condition test x != y, executes next instruction if true
ifgv			  | [label] [label]				| Performs a condition test x >  y, executes next instruction if true
iflv			  | [label] [label] 				| Performs a condition test x <  y, executes next instruction if true
draw		          | [x][y][label]		                | Writes data to vram from label with x and y coords
drawm		          | [x][y][label]		                | Writes data to vram from label as memory address with x and y coords
print			  | [x][y][index][label]			| Writes character index to vram from label as string with x and y coords
clear		          | no params			                | Clears vram memory (blanks screen)
end		          | no params 	                  		| Terminates execution of program
```

----------------------------------------------------------------------------------------------------
Sprites
----------------------------------------------------------------------------------------------------
Sprites can be made from a binary array of 8x6, compiler assumes only  1's and 0's in this order
```
jmp [program]

:x	10
:y	10

:program
	draw [x][y][letter_A]
	end


[letter_A]
	.00011000
	.00100100
	.01000010
	.01111110
	.01000010
	.01000010
```
This would produce the letter 'A' drawn on the screen at position variables x and y

----------------------------------------------------------------------------------------------------
Variables
----------------------------------------------------------------------------------------------------
Variables can be defined with labels followed by number, the compiler assumes an UInt16 value
```
:program
	load [foo]
	push #5
	add
	store [foo]
	end
		
:foo	:5
```

Pseudo code:
```
declare i = 5
i += 5
exit 
```
----------------------------------------------------------------------------------------------------
Strings
----------------------------------------------------------------------------------------------------
String data is decoded by the assembler into font address values, the 'print' instruction can
then enumerate for each character and draw them on the given x and y position.

```
jmp [program]

:x	:1
:y	:1
:i	:0

:program
	print [x][y][i][string]
	end

:string @"A"
```
This would print the letter 'A' on the screen, because i is 0 and the string array begins with 0.

----------------------------------------------------------------------------------------------------
Loop example with increasing variable and condition test
----------------------------------------------------------------------------------------------------
```
:program
	if [foo] #10 end
	call [add]
	jmp [program]

:add
	load [i]
	push #1
	add
	store [i]
	return

:i	:0
```

Psuedo code:
```
declare i = 0

do
	if (i == 10)  {
		exit
	} else {
		i += 1
	}
loop
```


