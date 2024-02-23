// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

     
 


        const input = document.querySelector("#phone");
        const input_bp = document.querySelector("#phone-bp");
        const input_frd = document.querySelector("#phone-frd");
        const input_concierge = document.querySelector("#phone-concierge");

        if (input != null) {
   
            window.intlTelInput(input, {
                utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@19.2.16/build/js/utils.js",
            });
        }
        if (input_frd != null) {
            
            window.intlTelInput(input_frd, {
                     utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@19.2.16/build/js/utils.js",
                 });
        }
        if (input_concierge != null) {
            
            window.intlTelInput(input_concierge, {
                     utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@19.2.16/build/js/utils.js",
                 });
        }
        if (input_bp != null) {
           
            window.intlTelInput(input_bp, {
                      utilsScript: "https://cdn.jsdelivr.net/npm/intl-tel-input@19.2.16/build/js/utils.js",
                  });
        }


    