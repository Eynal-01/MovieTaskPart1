setInterval(InvokeHomeIndex, 5000);
setInterval(DeleteMovie, 10000);

function InvokeHomeIndex() {
    $.ajax({
        url: `/Home/GetMovie`,
        method: "GET",

        success: function (data) {
            var context = "";

            if (data.poster != null && data.name != null) {
                context = `
                 <div style="margin-top:8%;margin:auto;width:30%;height:50%;">
                      <h1 style="text-align:center;">${data.name}</h1>
                      <img style="width:100%;height:100%" src="${data.poster}" />
                 </div>
            `;
            }

            var movie = document.getElementById("movie");

            movie.innerHTML = context;
        }
    })
}

function DeleteMovie() {
    $.ajax({
        url: `/Home/DeleteMovie`,
        method: "GET",

        success: function (data) {
            //console.log("s");
        }
    })
}