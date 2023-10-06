
    $(document).ready(function () {
        // Click event for "Dashboard" link
        $("#dashboard-link").click(function () {
            $("#dashboard-content").html(
                `<div class="col-10" id="dashboard-content">
                <!-- Dashboard Content -->
               <div class="row col-md-12 ms-4">
                <div class="col-md-3 bg-primary sidebetweenbars ">
                    <div class="mt-3 text-white">Account Balance</div>
                        <hr />
                    <div class="text-white"></div>
                   </div>
                <div class="col-md-3 bg-success sidebetweenbars ">
                    <div class="mt-3 text-white ">Transfer</div>
                    <hr />
                    <div class="row mt-5 mb-2">
                        <div class ="col-md-8">
                            <a class=" text-white hyperlinkremove transfer-link">View details</a>
                        </div>
                        <div class="col-md-4">
                           <span class="text-white"><box-icon name='chevron-right'></box-icon></span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 bg-warning ">
                    <div class="mt-3 text-white">Profile</div>
                    <hr />
                    <div class="row mt-5 mb-2">
                        <div class="col-md-8">
                            <a class=" text-white hyperlinkremove">View details</a>
                        </div>
                        <div class="col-md-4">
                            <span class=" moveright text-white"><box-icon name='chevron-right'></box-icon></span>
                        </div>
                    </div>
                </div>
               </div>
            </div>`
            );
        });

    // Click event for "Transfer Money" link
    $(".transfer-link").click(function () {
        // Load the Transfer Money view or content here
        $("#dashboard-content").html(
            `<div class="row col-md-12">
                <div class= "col-12" > <span>Transfer Money</span></div>
                <hr/>
                   <div class= "row col-md-12">
                    <div class= "card col-md-6 sidebetweenbars ">
                        <div class="card-header row">
                        <span>Sourced Account</span>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                            
                            <label asp-for="@model.Account.Number" class=" text-muted"></label>
                             <input asp-for="@model.Account.Number" class="form-control disable" />
                            </div>
                            <div class="col-md-6">
                                <label asp-for="@model.Account.Name" class=" text-muted"></label>
                             <input asp-for="@model.Account.Name" class="form-control disable" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                            
                            <label  class=" text-muted">Amount To Transfer</label>
                             <input asp-for="@model.Account.Amount" class="form-control" />
                            </div>
                            
                        </div>
                   </div>
                    </div>

                    <div class= "card col-md-6">
                        <div class="card-header row">
                        <span>Destination Account</span>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                            
                            <label asp-for="@model.Account.Number" class=" text-muted"></label>
                             <input asp-for="@model.Account.Number" class="form-control" />
                            </div>
                            <div class="col-md-2">
                                
                             <button>Search</button>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                            
                            <label  class=" text-muted">Amount Name</label>
                             <input asp-for="@model.Account.Name" class="form-control disabled" />
                            </div>
                            
                            <div class="col-md-6">
                            
                            <label  class="text-muted">Description</label>
                             <input asp-for="@model.Account.TransactionDescription" class="form-control" />
                            </div>
                        </div>
                   </div>
                    </div>
                   </div>
            
            </div>`
           
        );
        });

        // Add similar click events for other menu items
    });





