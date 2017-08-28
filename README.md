# Pixel

A small 16bit Virtual Machine with assembler to create small programs or games

![](http://i.imgur.com/OpMIVK3.png)

Memory Visualization

![](http://i.imgur.com/Fg7wTpx.png)

Example of 'Hello, World' in Pixel language

```
;************************************
; Program: Hello, World!
;************************************

include print

clear | jmp [main]

:var	"Hello, World!"

:main
	push #20
	push #20
	push [var]
	call [@print]
	end
```

Instructions
```
Function	      	  | Parameters                    		| Description
--------------------------+---------------------------------------------+---------------------------------------------------
push 		          | (#<num>|0x<hex>|[label])			| Loads value onto the stack, labels will translate as label address
pop		          | no params			                | Pops value from the stack
load		          | ([label])			                | Loads variable onto the stack
store		          | ([label])			                | Pops value from stack and stores to variable
storev		          | ([label]) (#<num>|0x<hex>)	                | Stores value to variable
jmp		          | ([label])			                | Jumps to label location
call		          | ([label])			                | Call procedure
return		      	  | no params			                | Returns from procedure
rsp			  | no params					| Reset stack pointer to zero
add		          | no params			                | Performs an addition of the two stack values
sub		          | no params			                | Performs a subtraction of the two stack values
mul		          | no params			                | Performs a multiplication of the two stack values
div		          | no params			                | Performs a division of the two stack values
mod		          | no params			                | Performs a modulus of the two stack values
inc			  | ([label])  (#<num>|0x<hex>)			| Increases variable by given number
dec			  | ([label])  (#<num>|0x<hex>)			| Decreases variable by given number
and			  | no params			                | Performs a bitwise AND of the two stack values
or		          | no params			                | Performs a bitwise OR of the two stack values
xor		          | no params			                | Performs a bitwise XOR of the two stack values
shr		          | no params			                | Performs a bitwise shift right of the two stack values
shl		          | no params			                | Performs a bitwise shift left of the two stack values
stov			  | ([label])					| Stores overflow value to variable
addressOf		  | ([label])					| Pushes memory address of label onto the stack
writeAt			  | ([label])					| Writes stack value to memory address
readAt			  | ([label])					| Reads memory address and pushes value onto the stack
seed			  | (#number)					| Number used to calculate a starting value for the pseudo-random number sequence (0 = default)
random			  | (#<num>|0x<hex>)				| Pushes random number, starting from 0 and the parameter defines max range
if			  | [label] (#number)				| Condition test x == y, executes next instruction if true
ifn			  | [label] (#number) 				| Condition test x != y, executes next instruction if true
ifg			  | [label] (#number)				| Condition test x >  y, executes next instruction if true
ifl			  | [label] (#number) 				| Condition test x <  y, executes next instruction if true
ifv			  | [label] [label]				| Condition test x == y, executes next instruction if true
ifnv			  | [label] [label] 				| Condition test x != y, executes next instruction if true
ifgv			  | [label] [label]				| Condition test x >  y, executes next instruction if true
iflv			  | [label] [label] 				| Condition test x <  y, executes next instruction if true
draw		          | [x][y][label]		                | Draw sprite to vram from defined label at x and y
print			  | [x][y][index][label]			| Writes character index to vram from label as string with x and y coords
printv			  | [x][y][var]					| Writes numeric value of a variable as string, same as 'print' without the index
stcol			  | ([label])					| Stores collision value to variable (1 or 0)
strlen			  | ([label])([var])				| Stores string length in variable
strcmp			  | ([string])([string])			| Performs a condition test string == string, executes next instruction if true
input			  | ([label])					| Stores keystroke value to variable
clear		          | no params			                | Clears vram memory (blanks screen)
end		          | no params 	                  		| Terminates execution of program
--------------------------+---------------------------------------------+---------------------------------------------------
_drawa			  | [x][y][address]				| Internally used opcode for drawing a sprite given by address
_print			  | [x][y][address]				| Internally used opcode for printing a string given by address
_printv			  | [x][y][address]				| Internally used opcode for printing a variable number given by address
_strla			  | [address][variable]				| Internally used opcode for obtaining a string length given by address
--------------------------+---------------------------------------------+---------------------------------------------------
include			  | <print|common>				| Import build-in functions
```

----------------------------------------------------------------------------------------------------
Limitations
----------------------------------------------------------------------------------------------------
The 'if' and 'strcmp' type statements assume the skipped instruction to be 3 bytes, this means instructions
that take up more bytes will not work.

The following instructions do not work with if/strcmp -statement:
- print
- printv
- draw
- inc/dec
- if-types
- strcmp
- storev

----------------------------------------------------------------------------------------------------
Import Build-In functions
----------------------------------------------------------------------------------------------------

Pixel has build-in functions that simplify the making of certain procedures, 
these functions are included into the user assembly.


[@print]
Simplified version of the print.

Params: x, y, [address]

```
include print

:string	"Hello!"

push #10
push #10
push [string]
call [@print]
```

[@printv]
Simplified version of the printv.

Params: x, y, [address]

```
include print

:number	100

push #10
push #10
push [number]
call [@printv]
```

[@sleep]
Delays the current procedure by given number of cycles.

Params: int
```
include common

push #60
call [@sleep]
```

[@scroll]
Scrolls display to specified direction and steps

Params: direction steps
```
include common

push #0
push #1
call [@scroll]
```

----------------------------------------------------------------------------------------------------
Sprites
----------------------------------------------------------------------------------------------------
Sprites can be made from a binary array that is 8x6, the format is shown below.

```
jmp [program]

:x	10
:y	10

:program
	draw [x][y][letter_A]
	end


:letter_A
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
Variables can be defined with labels followed by a number, the compiler assumes an UInt16 value.
The value of a variable must be in straight forward format, no hexadecimals or such.

```
:program
	load [foo]
	inc [foo] #5
	end

:foo	5
```

Pseudo code:
```
declare foo = 5
foo += 5
exit 
```
----------------------------------------------------------------------------------------------------
Strings
----------------------------------------------------------------------------------------------------
String data is decoded by the assembler into font address values, the 'print' instruction can
then enumerate for each character index and draw them on the given x and y position.

```
include print

:foo	"Hello!"

:program
	push #10	;push x
	push #10	;push y
	push [foo]	;push address
	call [@print]	;call built-in print function
	end
```

This process can also be achieved manually by doing the following

```
:x	10		;variable block
:y	10
:i	0
:len	0
:str	"Hello!"

:program
	strlen [str][len]	;store string length
:loop
	if [i][len] end		;condition test for length, if reached, end program
	print [x][y][i][str]	;print out character on i in string
	inc [i] #1		;increase index
	inc [x] #6		;increase x
	jmp [loop]		;loop back...
```

----------------------------------------------------------------------------------------------------
Printing numeric values of variables
----------------------------------------------------------------------------------------------------
In order to print out a value of a variable we use a different print command called printv

```
include print

:number	100

:program
	push #10
	push #10
	push [number]
	call [@printv]	
	end
```

This process can also be achieved manually by doing the following

```
:x	10
:y	10
:number	100

:program
	printv [x][y][number]
	end
```

----------------------------------------------------------------------------------------------------
keyboard input
----------------------------------------------------------------------------------------------------
Keys can be defined with { and }, the assembler will convert the keys to memory address values
that corresponds with an address offset within the memory data.
The format is the same as defining variables except you set your keys between the braces.

The instruction 'key [var]' will store the current keystroke that is equal to the defined key variable.
If no keystroke was recorded, it will store a 0 instead.

Supported characters:
```
abcdefghijklmnopqrstuvwxyz
1234567890 +-*/[|]!?,.:<>\'↑↓←→
```

Example of a simple key hook
```
:program
	input [key]
	if [key] [exit] end
	jmp [program]

:exit		{Q}
:key		0
```

Psuedo code:
```
declare exit = key{q}
declare input = 0

do
	input = key_press()
	if (input == exit) { exit }
Loop
```
