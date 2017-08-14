# Pixel [Work in Progress]
Tiny 16bit Virtual Machine

The current assembler is not a final solution, just a quick work and test, 
bear in mind that many aspects are subject to change along the way of development, frequent updates are to be expected.

![](http://i.imgur.com/nccezv3.png)

Example of 'Hello, World' in Pixel language

```
;************************************
; Program: Hello, World!
;************************************
jmp [main]


:x		50
:y		50
:i		0
:str		"Hello, World!"

:main
	call [next]
	if [i] #13 end
	jmp [main]

:next
	print [x][y][i][str]
	load [i]
	push #1
	add
	store [i]
	load [x]
	push #5
	add
	store [x]
	return
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
stov			  | ([label])					| Stores overflow value to variable
addressOf		  | ([label])					| Pushes memory address of label onto the stack
writeAt			  | ([label])					| Writes stack value to memory address
readAt			  | ([label])					| Reads memory address and pushes value onto the stack
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
print			  | [x][y][index][label]			| Writes character index to vram from label as string with x and y coords
stcol			  | ([label])					| Stores collision value to variable (1 or 0)
key			  | ([label])					| Stores keystroke value to variable
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
Variables can be defined with labels followed by number, the compiler assumes an UInt16 value
```
:program
	load [foo]
	push #5
	add
	store [foo]
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
jmp [program]

:x		:1
:y		:1
:i		:0
:string 	"A"

:program
	print [x][y][i][string]
	end


```
This would print the letter 'A' on the screen, because i is 0 and the string array begins with 0.

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
	key [input]
	if [input] [exit] end
	jmp [program]

:exit		{Q}
:input		0
```

Psuedo code:
```
declare exit = key{q}
declare input = 0

do
	if (input == exit) { exit }
Loop
```

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


