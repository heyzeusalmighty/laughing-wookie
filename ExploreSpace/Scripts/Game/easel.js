
$(document).ready(function() {


    //globals
    var divisionOne = "rgba(103,155,153,0.8)";
    var divisionTwo = "rgba(65,129,127,0.8)";
    var divisionThree = "rgba(13, 77, 75, 0.8)";
    var background = "rgba(43, 62, 80, 1)";
    var occupied = "rgba(13,39,74,0.8)";
    var orange = "#bf5a16";
    var brown = "#532709";
    var pink = "#D46A6A";
    var tiles = [];
    var ships = [];
    

    var stage = new createjs.Stage("demoCanvas");
    buildHexGrid();
    buildActionBar();

    function init() {
        // code here.
        console.log('shazam');
        


        var circle = new createjs.Shape();
        circle.graphics.beginFill("DeepSkyBlue").drawCircle(0, 0, 50);
        circle.x = 100;
        circle.y = 100;

        //circle.addEventListener("click", function (event) { console.log('clicked'); });
        //circle.on("click", function(evt) {
        //    console.info("type: "+evt.type+" target: "+evt.target+" stageX: "+evt.stageX);
        //});

        circle.on("pressmove", function (evt) {
            evt.target.x = evt.stageX;
            evt.target.y = evt.stageY;
            stage.update();
        });
        circle.on("pressup", function(evt) { console.log("up"); });

        stage.addChild(circle);

        stage.update();
    }

     function buildActionBar() {

         var explore = buildActionButtons("EXP", 5, 10);
         var influence = buildActionButtons("INF", 70, 10);
         var research = buildActionButtons("RES", 135, 10);
         var upgrade = buildActionButtons("UPG", 200, 10);
         var build = buildActionButtons("BUI", 265, 10);
         var move = buildActionButtons("MOV", 330, 10);


         var discs = 13;
         var startX = 415;
         for (var i = 0; i < 13; i++) {
             buildIncomeDisc(true, startX, 25, i + 1, 20);
             startX += 46;
         }

         stage.update();
     }

    function buildActionButtons(name, x, y) {
        var background = new createjs.Shape();
        background.name = "background";
        background.graphics.beginFill("orange").drawRoundRect(0, 0, 50, 30, 4);

        var label = new createjs.Text(name, "bold 14px Arial", "#FFFFFF");
        label.name = "label";
        label.textAlign = "center";
        label.textBaseline = "middle";
        label.x = 50 / 2;
        label.y = 30 / 2;

        var button = new createjs.Container();
        button.name = "button";
        button.x = x;
        button.y = y;
        button.addChild(background, label);
        button.mouseChildren = false;

        button.on("click", function (evt) {
            console.info(name);
        });

        stage.addChild(button);
    }

    function buildIncomeDisc(filled, x, y, value, radius) {
        var circle = new createjs.Shape();
        circle.graphics.beginFill("DeepSkyBlue").drawCircle(0, 0, radius);
        circle.x = x;
        circle.y = y;

        var label = new createjs.Text(value, "bold 12px Arial", "#FFFFFF");
        label.name = "label";
        label.textAlign = "center";
        label.textBaseline = "middle";
        label.x = x;
        label.y = y;


        stage.addChild(circle, label);
    }
    
    function buildHexGrid() {
        


        $('#loadingSpinner').show();

        var radius = 45;
        var sx = 6;
        var sy = 5;
        var cols = 13;
        var rows = 11;

        var hexagonGrid = new HexGrid(stage, radius, background, orange, brown, pink, cols, rows, false);


        $.ajax({ url: "/api/map" }).done(function (data) {
            $('#loadingSpinner').hide();
            //hexagonGrid.processShips(data.Ships);
            tiles = data.MapTiles;
            processMapTiles();
            hexagonGrid.buildGameHexes(tiles);
        });


        
        
        
        //hexagonGrid.buildGameHexes();
    }


    function processMapTiles() {
        //console.log(tilez);
        for (var i = 0; i < tiles.length; i++) {

            switch (tiles[i].Division) {
                case 1:
                    tiles[i].color = divisionOne;
                    break;
                case 2:
                    tiles[i].color = divisionTwo;
                    break;
                case 3:
                    tiles[i].color = divisionThree;
                    break;
                default:
                    console.log('division not found');
            }

            //hexagonGrid.buildGameHex(tiles[i]);
        }
        
    }

});


