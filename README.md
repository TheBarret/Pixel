# Pixel [Work in Progress]
Tiny 16bit Virtual Machine

![](http://i.imgur.com/lRFy2gI.png)


Instructions
```
----------------------------------------------------------------------------------------------------
Function	      | Parameters                    | Description
----------------+-------------------------------+---------------------------------------------------
push 		        | (#<num>|0x<hex>|[label])	    | Loads value onto the stack
pop		          | no params			                | Pops value from the stack
ld		          | ([label])			                | Loads defined variable onto the stack
st		          | ([label])			                | Stores value from stack into defined variable
jmp		          | ([label])			                | Jumps to label location
call		        | ([label])			                | Sets pointer to label location
return		      | no params			                | Returns pointer to caller
add		          | no params			                | Performs an addition of the two stack values
sub		          | no params			                | Performs a subtraction of the two stack values
mul		          | no params			                | Performs a multiplication of the two stack values
div		          | no params			                | Performs a division of the two stack values
mod		          | no params			                | Performs a modulus of the two stack values
and	          	| no params			                | Performs a bitwise AND of the two stack values
or		          | no params			                | Performs a bitwise OR of the two stack values
xor		          | no params			                | Performs a bitwise XOR of the two stack values
shr		          | no params			                | Performs a bitwise shift right of the two stack values
shl		          | no params			                | Performs a bitwise shift left of the two stack values
if		          | (#number) (instruction)	      | Performs a condition test x =  y on parameter and stack value, executes next instruction if true
ifn		          | (#number) (instruction)	      | Performs a condition test x != y on parameter and stack value, executes next instruction if true
ifg		          | (#number) (instruction)	      | Performs a condition test x >  y on parameter and stack value, executes next instruction if true
ifl		          | (#number) (instruction)	      | Performs a condition test x <  y on parameter and stack value, executes next instruction if true
ifk		          | (#number) (instruction)	      | Performs a condition if key is pressed, executes next instruction if true
timer		        | (#number)			                | Sets delay timer with value
updd		        | ([label])			                | Loads data from label with binary data into vram, assumes x and y are on the stack
updm		        | ([label])			                | Loads data from label with memory address into vram, assumes x and y are on the stack
clear		        | no params			                | Clears vram memory (blanks screen)
end		          | no params 	                  | Terminates execution of program
```

----------------------------------------------------------------------------------------------------
Sprites
----------------------------------------------------------------------------------------------------
Sprites can be made from a binary array of 8x6, compiler assumes only  1's and 0's in this order
```
:program
	push #1
	push #1
	updd [letter_A]
	end

:letter_A
	.00011000
	.00100100
	.01000010
	.01111110
	.01000010
	.01000010
```
This would produce the letter 'A' drawn on the screen at position x=1 y=1

----------------------------------------------------------------------------------------------------
Variables
----------------------------------------------------------------------------------------------------
Variables can be defined with labels followed by number, the compiler assumes an UInt16 value
```
:program
	ld [foo]
	push #5
	add
	st [foo]
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
Loop example with increasing variable and condition test
----------------------------------------------------------------------------------------------------
```
:program
	ld [foo]
	if #10 end
	call [add]
	jmp [program]

:add
	ld [i]
	push #1
	add
	st [i]
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


