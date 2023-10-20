
$(document).ready(
    function () {
        // Click event for "Dashboard" link
        $("#dashboard-link").click(function () {

            
                $("#dashboard-content").html(
                    `
                <!-- Dashboard Content -->
               <div class="row col-md-12 ms-4">
                <div class="col-md-3 bg-primary sidebetweenbars  bordered-5">
                    <div class="mt-3 text-white">Account Balance</div>
                        <hr />
                    <div class="text-white"></div>
                   </div>
                <div class="col-md-3 bg-success sidebetweenbars ">
                    <div class="mt-3 text-white">Transfer</div>
                    <hr />
                    <div class="row mt-5 mb-2">
                        <div class ="col-md-10">
                            <a class="text-white ViewDetailstransfer">View details</a>
                        </div>
                        <div class="col-md-2">
                           <span class="text-white"><a class="transfer-link text-white ViewDetailstransfer"><box-icon name='chevron-right'></box-icon></a></span>
                        </div>
                    </div>
                </div>

                <div class="col-md-3 bg-warning ">
                    <div class="mt-3 text-white hyperlinkremove">Profile</div>
                    <hr />
                    <div class="row mt-5 mb-2">
                        <div class="col-md-10">
                            <a class=" text-white hyperlinkremove">View details</a>
                        </div>
                        <div class="col-md-2">
                            <span class=" moveright text-white"><box-icon name='chevron-right'></box-icon></span>
                        </div>
                    </div>
                </div>
               </div>
              </div>`
                );
            });

    // Click event for "Transfer Money" link
        $(".transfer-link").click(function showTransferTab() {
            // Load the Transfer Money view or content here
            $("#dashboard-content").html(
                `<div class="row col-md-12">
            <div class= "col-12" > <span>Transfer Money</span></div>
            <hr/>
        </div>
        <!--overall row-->
            <div class= "row col-md-12 ">
               <!--first box-->
                <div class= "card col-md-6 ms-3 "> 
                    <div class="card-header row  bg-info ">
                      <span>Sourced Account</span>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                    
                         <label class=" text-muted" asp-for="@Model.Account.Number">Account Number</label>
                         <input asp-for="@Model.Account.Number" class="form-control" disabled/>
                        </div>
                        <div class="col-md-12">
                            <label class=" text-muted" asp-for="@Model.Account.Name">Account Name</label>
                         <input asp-for="@Model.Account.Name" class="form-control" disabled/>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 mb-4">
                    
                         <label  class=" text-muted" asp-for="@Model.Account.Amount">Amount To Transfer</label>
                         <input asp-for="@Model.Account.Amount" class="form-control" />
                        </div>
                    
                    </div>
               </div>
               <!--first box end-->

                <!--second box-->
                            
                    <div class= "card col-md-6 ms-4 mt-3">
                        <div class="card-header row  bg-info ">
                           <span>Destination Account</span>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                        
                                <label asp-for="@model.Account.Number" class=" text-muted"></label>
                                <input asp-for="@model.Account.Number" class="form-control" oninput="fetchAccountName(this.value)"  />
                            </div>

                            <div class="col-md-12 mt-4">
                               <button>Search</button>
                            </div>
                        </div>

                        <div class="row mb-4 mt-4">
                            <div class="col-md-12">
                        
                              <label  class=" text-muted">Amount Name</label>
                              <input asp-for="@model.Account.Name" class="form-control disabled"  id="accountName"/>
                            </div>
                        
                            <div class="col-md-12">
                        
                              <label  class="text-muted">Description</label>
                              <input asp-for="@model.Account.TransactionDescription" class="form-control" />
                            </div>
                        </div>
                    </div>
                <!--second box end-->
                
                
            </div>
            
            <div class="row col-md-12 mt-4">
                <button class="bg-primary col-md-2 ms-3 ml-3 btn btn-primary">Transfer</button>
                <button class="bg-danger  col-md-2 btn btn-primary">Reset</button>
                </div>
            
            `
                
           
            );
        });

        

        


        // Add similar click events for other menu items
    });








