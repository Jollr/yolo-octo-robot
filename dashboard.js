var Refresher = function() {
	var state = '';
	
	var refresh = function(image) { 
        var d = new Date();
		var img = $(image);
        var src = img.attr('src').split('?')[0] + '?' + d.getTime();
        img.attr('src', src);
    };
	
	this.Update = function(newState) {
		if (state == '') { state = newState; }
		console.log(newState);
		
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
        $.get('/dashboard/puzzleState', refresher.Update);
    };
    window.setInterval(update, 500);
});