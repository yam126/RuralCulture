; (function ($) {
	'use strict';
	var css = document.createElement("link");
	var js = document.scripts;
	var path = js[js.length - 1].src.substring(0, js[js.length - 1].src.lastIndexOf("/") + 1);
	css.setAttribute("rel", "stylesheet");
	css.setAttribute("type", "text/css");
	css.setAttribute("href", path + "alert.css");
	document.head.appendChild(css);
	jQuery.extend({
		open: function (type, option) {
			var content = option ? option : '', title = '', confirmText = '确定', cancelText = '取消';
			if (typeof (option) == 'object') {
				title = option.title ? option.title : title;
				content = option.content ? option.content : content;
				confirmText = option.confirmText ? option.confirmText : confirmText;
				cancelText = option.cancelText ? option.cancelText : cancelText;
			}
			var html = "";
			html += '<div class="dialog-modal-mask" id="dialog-mask-' + type + '"></div>';
			html += '<div class="dialog-modal dialog-modal-' + type + '" id="dialog-' + type + '">';
			switch (type) {
				case 'alert':
					html += '<div class="dialog-modal-title">' + title + '</div>';
					html += '<div class="dialog-modal-text">' + content + '</div>';
					html += '<div class="dialog-modal-btn">';
					html += '<button type="button">' + confirmText + '</button>';
					break;
				case 'confirm':
					html += '<div class="dialog-modal-title">' + title + '</div>';
					html += '<div class="dialog-modal-text">' + content + '</div>';
					html += '<div class="dialog-modal-btn">';
					html += '<button type="button class="cancel">' + cancelText + '</button>';
					html += '<button type="button" class="confirm">' + confirmText + '</button>';
					break;
				case 'toast':
					html += '<div class="dialog-modal-text">' + content + '</div>';
					break;
				case 'loading':
					html += '<div class="dialog-modal-load"><span></span><span></span><span></span></div>';
					html += '<div class="dialog-modal-text">' + content + '</div>';
					break;
			}
			$("body").addClass('overflow').append(html);
			window.addEventListener("mousewheel", this.forbidScroll);
			window.addEventListener("touchmove", this.forbidScroll, { passive: false });
			var obj = $("#dialog-" + type);
			$("#dialog-mask-" + type).fadeIn(300);
			obj.addClass("dialog-ani-open");
			var screenHeight = window.screen.height;
			var innerHeight = window.innerHeight;
			var height = obj.height();
			obj.css("margin-top", "-" + Math.ceil((screenHeight - innerHeight + height) / 2) + 'px');
			obj.show();
		},
		alert: function (option, callback) {
			this.open('alert', option);
			let that = this;
			$('.dialog-modal-btn button').click(function () {
				that.close("alert");
				if (typeof (callback) != 'undefined') {
					callback();
				}
			})
		},
		confirm: function (option, confirm, cancel) {
			this.open('confirm', option);
			let that = this;
			$('.dialog-modal-btn button').click(function () {
				that.close("confirm");
				if ($(this).attr('class') === 'confirm') {
					if (typeof (confirm) != 'undefined') confirm();
				} else {
					if (typeof (cancel) != 'undefined') cancel();
				}
			})
		},
		toast: function (option, callback) {
			this.open('toast', option);
			var time = option.time ? option.time : 1500;
			setTimeout(() => {
				this.close('toast');
				if (typeof (callback) != 'undefined') {
					callback();
				}
			}, time)
		},
		loading: function (option) {
			this.open('loading', option);
		},
		hideLoading: function (callback) {
			this.close("loading");
			if (typeof (callback) != 'undefined') {
				callback();
			}
		},
		close: function (type) {
			window.removeEventListener("mousewheel", this.forbidScroll);
			window.removeEventListener("touchmove", this.forbidScroll, { passive: false });
			$("body").removeClass('overflow');
			var obj = $("#dialog-" + type);
			$("#dialog-mask-" + type).fadeOut(200);
			obj.addClass("dialog-ani-close");
			setTimeout(() => {
				obj.hide();
				obj.removeClass("dialog-ani-close");
				$("#dialog-mask-" + type).remove();
				obj.remove();
			}, 300)
		},

	})
})(jQuery);