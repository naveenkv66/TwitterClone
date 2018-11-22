$(document).ready(function () {
    $('#registerFormId').validate({
        errorClass: 'help-block animation-slideDown', // You can change the animation class for a different entrance animation - check animations page  
        errorElement: 'div',
        errorPlacement: function (error, e) {
            e.parents('.form-group > div').append(error);
        },
        highlight: function (e) {

            $(e).closest('.form-group').removeClass('has-success has-error').addClass('has-error');
            $(e).closest('.help-block').remove();
        },
        success: function (e) {
            e.closest('.form-group').removeClass('has-success has-error');
            e.closest('.help-block').remove();
        },
        rules: {
            'UserName': {
                required: true
            },

            'Password': {
                required: true,
                minlength: 6
            },

            'FullName': {
                required: true
            },

            'Email': {
                required: true,
                email: true
            }

        },
        messages: {
            'Email': 'Please enter valid email address',

            'Password': {
                required: 'Please provide a password',
                minlength: 'Your password must be at least 6 characters long'
            }
        }
    });
});   