;*************************************************************
; NAME			: PRINT
; DESCRIPTION	: PRINTS STRING ON COORDINATES
; PARAMS		: X Y [ADDRESS]
;*************************************************************
:@print
	store [@print_addr]
	store [@print_y]
	store [@print_x]
	_strla [@print_addr][@print_len]
:@print_loop
	call [@print_next_char]
	ifv [@print_i][@print_len] jmp [@print_cleanup]
	jmp [@print_loop]
:@print_next_char
	_printa [@print_x][@print_y][@print_i][@print_addr]
	inc [@print_i] #1
	inc [@print_x] #6
	return
:@print_cleanup
	storev [@print_len] #0
	storev [@print_x] #0
	storev [@print_y] #0
	storev [@print_i] #0
	return
:@print_addr	0
:@print_len		0
:@print_x		0
:@print_y		0
:@print_i		0