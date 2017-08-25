;*************************************************************
; NAME			: PRINTV
; DESCRIPTION	: PRINTS VARIABLE NUMBER ON COORDINATES
; PARAMS		: X Y [ADDRESS]
;*************************************************************
:@printv
	store [@printv_var]
	store [@printv_y]
	store [@printv_x]
	_printav [@printv_x][@printv_y][@printv_var]
	storev [@printv_var] #0
	storev [@printv_x] #0
	storev [@printv_y] #0
	return
:@printv_var	0
:@printv_x		0
:@printv_y		0