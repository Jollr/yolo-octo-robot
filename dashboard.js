var Refresher = function() {
	var state = '';
	var finished = false;
	
	var refresh = function(image) { 
        var d = new Date();
		var img = $(image);
        var src = img.attr('src').split('?')[0] + '?' + d.getTime();
        img.attr('src', src);
    };
	
	this.Update = function(newState) {
		if (finished) {
			return;
		}
		if (state == '') { state = newState; }
		console.log(newState);
		
		if (newState == '1111111111111111111111111') {
		//if (newState == '0000000000001000000100010') {
			$('.puzzle').hide();
			$('.finished-gif').attr('src', 'eindgif.gif');
			window.setTimeout(function() {
				$('.finished-gif').attr('src', 'eindframe.png');
			}, 11000);
			
			finished = true;
		}
		for (var n = 0; n < state.length; n++) {
			if (state.charAt(n) != newState.charAt(n)) {
				refresh($('img')[n]);
			}
		}
		
		state = newState;
	};
};

$(function() {
    var refresher = new Refresher();
    var update = function() {
		var url = '/dashboard/puzzleState' + '?' + new Date().getTime();
        $.get(url, refresher.Update);
    };
    window.setInterval(update, 500);
});