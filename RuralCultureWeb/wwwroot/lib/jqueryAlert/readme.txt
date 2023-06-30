$.alert('弹窗文字说明', function() {
    $.toast('close');
});
$.confirm('弹窗文字说明', function() {
    $.toast('confirm');
}, function() {
    $.toast('cancel');
});
$.toast({
    content: 'toast弹窗',
    time: 3000
}, function() {
    console.log('close');
}); 
$.loading('加载中');
setTimeout(() => {
    $.hideLoading();
}, 3000); 
var html = '<input type="password" name="pwd" placeholder="请输入"  />';
$.confirm({
    title: '信息标题',
    content: html
}, function() {
    var password = $('input[name="pwd"]').val();
    if (!password) {
        $.toast('信息内容');
        return false;
    }
    $.toast(password);
});