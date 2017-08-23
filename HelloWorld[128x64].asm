;************************************
; Program: Hello, World!
;************************************
jmp [main]

:x		32
:y		32
:i		0
:len	0
:str		"Hello, World!"

:main	call [next]
	strlen [str][len]
	ifv [i][len] end
	jmp [main]
:next	print [x][y][i][str]
	inc [i] #1
	inc [x] #5
	return