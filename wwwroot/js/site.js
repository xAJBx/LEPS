// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function prependEventCard(num){
    num =+ 1
    $(`
    <br/>
    <div class="card event-card" style="width: 18rem;">
        <form method="post"
                <div class="card-body">
                    <h5 class="card-title">Event</h5>
                    <div>
                        <i onclick="prependEventCard(${num})" style="float: right;" class="fa-solid fa-plus"></i>
                        <div>
                            <label for="event-name">Event Name</label>
                            <input id="event-name" name="eventName${num}" />
                        </div>
                        <div>
                            <label for="event-cost">Entry Fee</label>
                            <input id="event-cost" name="eventCost${num}" />
                        </div>
                        <div>
                            <label for="event-date">Date</label>
                            <input id="event-date" type="datetime-local" name="eventDate${num}" />
                        </div>
                    </div>
                </div>
                </form>
            </div>    
`).insertAfter($(".event-card").last())
}
