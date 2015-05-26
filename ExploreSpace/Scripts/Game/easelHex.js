function HexGrid(stage, radius, background, orange, brown, pink, cols, rows, explore) {
    //this.radius = radius;
    this.radius = radius;
    this.background = background;
    this.orange = orange;
    this.brown = brown;
    this.pink = pink;
    this.softWhite = "rgba(103,155,153,0.2)";
    this.bigHexUp = false;
    this.gameTiles = [];
    this.playerShips = [];
    this.cols = cols;
    this.rows = rows;
    this.explore = explore;
    this.exploreMode = false;
    this.bigTile = 0;
    this.gameId = 'game';
    this.exploreTiles = [];
    this.playerName = "";
    this.selectedTile = {};
    this.wormIndex = 0;

    //Player Colors
    this.greenPlayer = "#2C8437";
    this.blackPlayer = "#010101";
    this.bluePlayer = "#2F4FD4";
    this.redPlayer = "#D10F40";
    this.yellowPlayer = "#FFF51D";
    this.whitePlayer = "#C4C2B6";
    this.central = "rgba(215,40,40,0.8)";
    this.holeColor = "#D4D4D4";
    this.outline = "#000000";
    this.background = "rgba(225,93,15,0.2)";

    //images 
    this.alienHead = new Image();
    this.alienHead.src = '../Content/Images/alienHeadx25.png';
    this.whiteShip = new Image();
    this.whiteShip.src = '../Content/Images/rocket-white-x25.png';
    this.blackShip = new Image();
    this.blackShip.src = '../Content/Images/rocketx25.png';
    this.advancedIncome = new Image();
    this.advancedIncome.src = '../Content/Images/AdvancedStar.png';
    this.viking = new Image();
    this.viking.src = '../Content/Images/viking-x100.jpg';

    this.setNewHexDimensions();

    this.stage = stage;
    this.stage.enableMouseOver(10);

    this.canvasOriginX = 0;
    this.canvasOriginY = 0;

    this.drawHexGrid(this.radius, this.radius, this.explore);

    //createjs.Ticker.addEventListener("tick", stage);

}

HexGrid.prototype.setGameIdentifier = function(game) {
    this.gameId = game;
};

HexGrid.prototype.setPlayerName = function(name, playerId) {
    this.playerName = name;
    this.drawGameBoard();
    console.info("current", playerId);
    for (var i = 0; i < this.gameTiles.length; i++) {
        if (!this.gameTiles[i].IsSet && this.gameTiles[i].PlayerId == playerId) {
            this.wormIndex = 0;
            //this.drawGameBoard();
            this.setWormholesOnTile(this.gameTiles[i]);
            break;
        }
    }


};

HexGrid.prototype.setNewHexDimensions = function () {
    this.height = Math.sqrt(3) * this.radius;
    this.width = 2 * this.radius;
    this.side = (3 / 2) * this.radius;
};

HexGrid.prototype.drawHexGrid = function (originX, originY, explore) {
    this.canvasOriginX = originX;
    this.canvasOriginY = originY;

    var cols = this.cols;
    var rows = this.rows;

    var currentHexX;
    var currentHexY;
    var debugText = "";

    var offsetColumn = false;

    for (var col = 0; col < cols; col++) {
        for (var row = 0; row < rows; row++) {

            if (!offsetColumn) {
                currentHexX = (col * this.side) + originX;
                currentHexY = (row * this.height) + originY;
            } else {
                currentHexX = col * this.side + originX;
                currentHexY = (row * this.height) + originY + (this.height * 0.5);
            }

            if (explore) {
                debugText = this.whatDivAmI(col, row);
            } else {
                debugText = col + "," + row;
            }


            //debugText = col + "," + row;

            this.drawHex(currentHexX, currentHexY, this.softWhite, debugText);
        }
        offsetColumn = !offsetColumn;
    }
};

HexGrid.prototype.drawHex = function (x0, y0, fillColor, debugText) {

    var polygon = new createjs.Shape();
    //polygon.graphics.setLineDash([5, 2]);
    
    polygon.graphics.beginStroke("#003432");
    polygon.graphics.moveTo(x0 + this.width - this.side, y0)
        .lineTo(x0 + this.side, y0)
        .lineTo(x0 + this.width, y0 + (this.height / 2))
        .lineTo(x0 + this.side, y0 + this.height)
        .lineTo(x0 + this.width - this.side, y0 + this.height)
        .lineTo(x0, y0 + (this.height / 2));
    polygon.graphics.closePath();

    //if (fillColor) {
    //    polygon.beginFill(fillColor);
    //}

    var label;
    if (this.explore) {

        label = new createjs.Text(debugText, "Bold 36px Sans-Serif", "rgb(158,101,101,0.5)");
        label.name = "label";
        label.textAlign = "center";
        label.textBaseline = "middle";
        label.x = x0 + (this.width / 2) - 10;
        label.y = y0 + (this.height - 25);

    } else {
        label = new createjs.Text(debugText, "10px Sans-Serif", "#000");
        label.name = "label";
        label.textAlign = "center";
        label.textBaseline = "middle";
        label.x = x0 + (this.width * (2/3));
        label.y = y0 + (this.height - 5);
        
    }

    this.stage.addChild(polygon, label);


    
};

HexGrid.prototype.buildGameHexes = function(tiles, ships) {
    this.playerShips = ships;
    this.gameTiles = tiles;
    this.drawGameBoard();
};

HexGrid.prototype.drawGameBoard = function() {
    this.stage.removeAllChildren();
    this.stage.update();
    this.buildActionBar();
    this.drawHexGrid(this.radius, this.radius, this.explore);
    var tiles = this.gameTiles;
    for (var i = 0; i < tiles.length; i++) {
        this.drawGameTile(tiles[i]);
    }
    this.stage.update();
};

HexGrid.prototype.buildActionBar = function() {
    var explore = this.buildActionButton("EXP", 5, 10);
    var influence = this.buildActionButton("INF", 70, 10);
    var research = this.buildActionButton("RES", 135, 10);
    var upgrade = this.buildActionButton("UPG", 200, 10);
    var build = this.buildActionButton("BUI", 265, 10);
    var move = this.buildActionButton("MOV", 330, 10);


    var discs = 13;
    var startX = 415;
    for (var i = 0; i < 13; i++) {
        this.buildIncomeDisc(true, startX, 25, i + 1, 20);
        startX += 46;
    }

    this.stage.update();
};

HexGrid.prototype.buildIncomeDisc = function (filled, x, y, value, radius) {
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


    this.stage.addChild(circle, label);
}

HexGrid.prototype.buildActionButton = function(name, x, y) {
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
    button.name = name;
    button.x = x;
    button.y = y;
    button.addChild(background, label);
    button.mouseChildren = false;
    
    button.addEventListener("click", this.takeAction.bind(this));

    this.stage.addChild(button);
};

HexGrid.prototype.drawGameTile = function(tile) {
    var y0 = tile.x % 2 == 0 ? (tile.y * this.height) + this.canvasOriginY : (tile.y * this.height) + this.canvasOriginY + (this.height / 2);
    var x0 = (tile.x * this.side) + this.canvasOriginX;
    
    if (tile.Occupied === 'Central') {
        this.buildCentralHex(tile);
        return false;
    }

    var playerColor = this.getColor(tile.Occupied);

    var polygon = new createjs.Shape();
    //polygon.graphics.beginStroke("#003432").beginFill(playerColor);
    polygon.graphics.beginStroke(playerColor).setStrokeStyle(3).beginFill("#004E4B");
    polygon.graphics.moveTo(x0 + this.width - this.side, y0)
        .lineTo(x0 + this.side, y0)
        .lineTo(x0 + this.width, y0 + (this.height / 2))
        .lineTo(x0 + this.side, y0 + this.height)
        .lineTo(x0 + this.width - this.side, y0 + this.height)
        .lineTo(x0, y0 + (this.height / 2));
    polygon.graphics.closePath();
    

    this.stage.addChild(polygon);

    
    if (tile.VictoryPoints) {
        var vicLbl = new createjs.Text(tile.VictoryPoints, "20px Sans-Serif", "#FFD300");
        vicLbl.name = "label";
        vicLbl.textAlign = "center";
        vicLbl.textBaseline = "middle";
        vicLbl.x = x0 + (this.width - 20);
        vicLbl.y = y0 + ((this.height / 2) + 5);
        this.stage.addChild(vicLbl);
    }

    //orange circle
    if (tile.Orange > 0) {
        this.drawOrangeSmall(x0, y0);
    }

    //brown circle
    if (tile.Brown > 0) {
        this.drawBrownSmall(x0, y0);
    }

    //pink circle
    if (tile.Pink > 0) {
        this.drawPinkSmall(x0, y0);
    }

    if (tile.White > 0) {
        this.drawWhiteSmall(x0, y0);
    }


    //ALIENS
    if (tile.Aliens > 0) {

        var alienX = x0 + (this.width - 75);
        var alienY = y0 + ((this.height / 2) - 10);

        if (tile.Aliens == 2) {
            var alien = new createjs.Bitmap(this.alienHead);
            alien.x = alienX;
            alien.y = alienY - 7;
            this.stage.addChild(alien);

            var aliens = new createjs.Bitmap(this.alienHead);
            aliens.x = alienX;
            aliens.y = alienY + 17;
            this.stage.addChild(aliens);
        } else {
            var bitmap = new createjs.Bitmap(this.alienHead);
            bitmap.x = alienX;
            bitmap.y = alienY;
            this.stage.addChild(bitmap);
        }

        
    }

    var containsShips = false;
    for (var s = 0; s < this.playerShips.length; s++) {
        //console.info('tile:' + tile.x + ',' + tile.y + ' :: ship: ' + this.playerShips[s].XCoords);
        if (tile.x == this.playerShips[s].XCoords && tile.y == this.playerShips[s].YCoords) {
            containsShips = true;
            break;
        }
    }

    if (containsShips) {

        //if (tile.Occupied == 'Black') {
            var ship = new createjs.Bitmap(this.whiteShip);
            ship.x = x0 + (this.width - 75);
            ship.y = y0 + ((this.height / 2) - 5);
            this.stage.addChild(ship);
            //console.info('wShip');
        //} else {
        //    var bShip = new createjs.Bitmap(this.blackShip);
        //    bShip.x = x0 + (this.width - 75);
        //    bShip.y = y0 + ((this.height / 2) - 5);
        //    this.stage.addChild(bShip);
        //    //console.info('bship');
        //}
    }

    //console.log(tile.Wormholes);
    this.drawWormHolesSmall(x0, y0, tile.Wormholes);

    //click event
    polygon.name = tile.MapId;
    polygon.addEventListener("click", this.examineMapTile.bind(this));
    polygon.mouseChildren = false;

    
};

HexGrid.prototype.buildCentralHex = function(tile) {

    var seven = createjs.Shape();
    seven.graphics.drawPolyStar(100, 100, 50, 7, 0.6, -90);

};

HexGrid.prototype.getColor = function (disc) {
    //console.log(disc);

    switch (disc) {
        case 'Black':
            return this.blackPlayer;
        case 'Green':
            return this.greenPlayer;
        case 'White':
            return this.whitePlayer;
        case 'Red':
            return this.redPlayer;
        case 'Blue':
            return this.bluePlayer;
        case 'Yellow':
            return this.yellowPlayer;
        case 'Central':
            return this.central;
        default:
            return this.background;
    }
};

HexGrid.prototype.drawOrangeSmall = function (x, y) {
    var circle = new createjs.Shape();
    circle.graphics.beginStroke("#000");
    circle.graphics.setStrokeStyle(1);
    circle.graphics.beginFill(this.orange).drawCircle(0, 0, 5);
    circle.x = x + (this.width - 40);
    circle.y = y + ((this.height / 2) + 10);
    this.stage.addChild(circle);
};

HexGrid.prototype.drawBrownSmall = function(x, y) {
    var circle = new createjs.Shape();
    circle.graphics.beginStroke("#000");
    circle.graphics.setStrokeStyle(1);
    circle.graphics.beginFill(this.brown).drawCircle(0, 0, 5);
    circle.x = x + (this.width - 40);
    circle.y = y + ((this.height / 2) - 5);
    this.stage.addChild(circle);
}

HexGrid.prototype.drawPinkSmall = function(x, y) {
    var circle = new createjs.Shape();
    circle.graphics.beginStroke("#000");
    circle.graphics.setStrokeStyle(1);
    circle.graphics.beginFill(this.pink).drawCircle(0, 0, 5);
    circle.x = x + (this.width - 40);
    circle.y = y + ((this.height / 2) - 20);
    this.stage.addChild(circle);
}

HexGrid.prototype.drawWhiteSmall = function(x, y) {
    var circle = new createjs.Shape();
    circle.graphics.beginStroke("#000");
    circle.graphics.setStrokeStyle(1);
    circle.graphics.beginFill(this.holeColor).drawCircle(0, 0, 5);
    circle.x = x + (this.width - 40);
    circle.y = y + ((this.height / 2) + 25);
    this.stage.addChild(circle);
};

HexGrid.prototype.examineMapTile = function(event) {

    console.info(event.target.name);
    var mapId = event.target.name;
    var tile = null;
    for (var i = 0; i < this.gameTiles.length; i++) {
        if (this.gameTiles[i].MapId == mapId) {
            tile = this.gameTiles[i];
        }
    }

    if (tile == null) {
        return false;
    }


    this.stage.removeAllChildren();
    this.stage.update();
    this.buildActionBar();

    var x0 = 100;
    var y0 = 100;
    var radius = 250;

    //var fillColor = "#003432";
    var fillColor = this.getColor(tile.Occupied);

    var height = Math.sqrt(3) * radius;
    var width = 2 * radius;
    var side = (3 / 2) * radius;

    var polygon = new createjs.Shape();
    polygon.graphics.beginStroke("#000000").beginFill(fillColor);
    polygon.graphics.moveTo(x0 + width - side, y0)
        .lineTo(x0 + side, y0)
        .lineTo(x0 + width, y0 + (height / 2))
        .lineTo(x0 + side, y0 + height)
        .lineTo(x0 + width - side, y0 + height)
        .lineTo(x0, y0 + (height / 2));
    polygon.graphics.closePath();
    this.stage.addChild(polygon);

    if (tile.VictoryPoints) {

        var crest = new createjs.Shape();
        crest.graphics.beginFill("red").drawRoundRect(x0 + 285, y0 + ((this.height / 2) - 10), 70, 70, 10);

        var vicLbl = new createjs.Text(tile.VictoryPoints, "Bold 72px Sans-Serif", "#FFD300");
        vicLbl.name = "label";
        vicLbl.textAlign = "center";
        vicLbl.textBaseline = "middle";

        vicLbl.x = x0 + 320;
        vicLbl.y = y0 + ((this.height / 2) + 25);
        this.stage.addChild(crest, vicLbl);
    }

    if (tile.Division) {
        var divLbl = new createjs.Text("Division " + tile.Division, "32px Sans-Serif", "#FFD300");
        divLbl.x = x0 + 50;
        divLbl.y = y0 + (height / 2) + 30;

        this.stage.addChild(divLbl);
    }

    this.drawWormHolesBig(x0, y0, tile.Wormholes);

    //Incomes

    var incomeX = x0 + 135;
    var incomeY = y0;

    if (tile.Pink > 0) {
        incomeY += 25;
        this.drawIncomeBig(incomeX, incomeY, tile.Pink, 0, this.pink);
    }

    if (tile.Orange > 0) {
        incomeY += 25;
        this.drawIncomeBig(incomeX, incomeY, tile.Orange, 0, this.orange);
    }

    if (tile.Brown > 0) {
        incomeY += 25;
        this.drawIncomeBig(incomeX, incomeY, tile.Brown, 0, this.brown);
    }

    if (tile.White > 0) {
        incomeY += 25;
        this.drawIncomeBig(incomeX, incomeY, tile.White, 0, this.white);
    }

    //reset coordinates
    var adIncomeX = x0 + 175;
    var adIncomeY = y0;

    if (tile.PinkAdvanced > 0) {
        adIncomeY += 25;
        this.drawAdvancedIncome(adIncomeX, adIncomeY, 1, 0, this.pink);
    }


    if (tile.OrangeAdvanced > 0) {
        adIncomeY += 25;
        this.drawAdvancedIncome(adIncomeX, adIncomeY, 1, 0, this.orange);
    }

    if (tile.BrownAdvanced > 0) {
        adIncomeY += 25;
        this.drawAdvancedIncome(adIncomeX, adIncomeY, 1, 0, this.brown);
    }

    console.info(tile);

    //SHIPS
    var shipCount = 0;

    var hexShips = { Interceptor : [], Cruiser : [], Dreadnought: []};
    for (var s = 0; s < this.playerShips.length; s++) {
        //console.info(s + ' => tile:' + column + ',' + row + ' :: ship: ' + this.playerShips[s].XCoords + ',' +this.playerShips[s].YCoords);
        if (tile.x == this.playerShips[s].XCoords && tile.y == this.playerShips[s].YCoords) {
            shipCount++;
            switch(this.playerShips[s].ShipType) {
                case "interceptor":
                    hexShips.Interceptor.push(this.playerShips[s]);
                    break;
                case "cruiser":
                    hexShips.Cruiser.push(this.playerShips[s]);
                    break;
                case "dreadnought":
                    hexShips.Dreadnought.push(this.playerShips[s]);
                    break;
                default:
                    console.info('Incorrect ShipType');
                    break;
            }

            //hexShips.push(this.playerShips[s]);
        }
    }

    if ( shipCount > 0) {

        var shipStartY = y0;
        var shipStartX = x0 + 550;

        var shipTitleLbl = new createjs.Text("Ships ", "32px Sans-Serif", "#000000");
        shipTitleLbl.x = shipStartX;
        shipTitleLbl.y = shipStartY;

        this.stage.addChild(shipTitleLbl);

        

        var interLbl = new createjs.Text("Interceptors : " + hexShips.Interceptor.length, "20px Sans-Serif", "#000000");
        interLbl.name = (hexShips.Interceptor.length > 0) ?
            hexShips.Interceptor[0].ShipId : -1;
        interLbl.x = shipStartX;
        interLbl.y = shipStartY + 40;
        interLbl.cursor = "pointer";
        interLbl.addEventListener("click", this.examineShip.bind(this));
        var interHit = new createjs.Shape();
        interHit.graphics.beginFill("#000").drawRect(0, 0, interLbl.getMeasuredWidth(), interLbl.getMeasuredHeight());
        interLbl.hitArea = interHit;
        this.stage.addChild(interLbl);


        var cruiserLbl = new createjs.Text("Cruisers : " + hexShips.Cruiser.length, "20px Sans-Serif", "#000000");
        cruiserLbl.name = (hexShips.Cruiser.length > 0) ?
            hexShips.Cruiser[0].ShipId : -1;
        cruiserLbl.x = shipStartX;
        cruiserLbl.y = shipStartY + 60;
        cruiserLbl.cursor = "pointer";
        cruiserLbl.addEventListener("click", this.examineShip.bind(this));
        var cruiserHit = new createjs.Shape();
        cruiserHit.graphics.beginFill("#000").drawRect(0, 0, cruiserLbl.getMeasuredWidth(), cruiserLbl.getMeasuredHeight());
        cruiserLbl.hitArea = cruiserHit;

        this.stage.addChild(cruiserLbl);

        var dreadLbl = new createjs.Text("Dreadnoughts : " + hexShips.Dreadnought.length, "20px Sans-Serif", "#000000");
        dreadLbl.name = (hexShips.Dreadnought.length > 0) ?
            hexShips.Dreadnought[0].ShipId : -1;
        dreadLbl.x = shipStartX;
        dreadLbl.y = shipStartY + 80;
        dreadLbl.cursor = "pointer";
        dreadLbl.addEventListener("click", this.examineShip.bind(this));
        var dreadHit = new createjs.Shape();
        dreadHit.graphics.beginFill("#000").drawRect(0, 0, dreadLbl.getMeasuredWidth(), dreadLbl.getMeasuredHeight());
        dreadLbl.hitArea = dreadHit;
        this.stage.addChild(dreadLbl);
        

    }


    this.drawCloseButton();


    //polygon.addEventListener("click", this.drawGameBoard.bind(this));
    //polygon.mouseChildren = false;
    
    this.stage.update();
};

HexGrid.prototype.drawWormHolesSmall = function(x, y, wormholes) {
    //var wormHoleLine = this.outline;


    //hole 0
    // hole[0]
    if (wormholes[0] === 1) {
        var holeOneX = x + (this.width - 45);
        var holeOneY = y;
        var zero = new createjs.Shape();
        zero.graphics.f(this.holeColor).s(this.outline);
        zero.graphics.arc(holeOneX, holeOneY, 7, 0, Math.PI);
        this.stage.addChild(zero);
    }

    if (wormholes[1] === 1) {
        var holeTwoX = x + (this.width - 12);
        var holeTwoY = y + 18;
        var one = new createjs.Shape();
        //this.context.arc(holeTwoX, holeTwoY, 7, Math.PI * 1.333, Math.PI * 0.333, true);
        one.graphics.f(this.holeColor).s(this.outline);
        one.graphics.arc(holeTwoX, holeTwoY, 7, Math.PI * 0.333, Math.PI * 1.333);
        this.stage.addChild(one);
    }

    if (wormholes[2] === 1) {
        var holeThreeX = x + (this.width - 12);
        var holeThreeY = y + 58;
        var two = new createjs.Shape();
        two.graphics.f(this.holeColor).s(this.outline);
        two.graphics.arc(holeThreeX, holeThreeY, 7, Math.PI * 0.666, Math.PI * 1.666);
        this.stage.addChild(two);
    }

    if (wormholes[3] === 1) {
        var holeFourX = x + (this.width - 45);
        var holeFourY = y + 78;
        var three = new createjs.Shape();
        three.graphics.f(this.holeColor).s(this.outline);
        three.graphics.arc(holeFourX, holeFourY, 7, Math.PI, 0);
        this.stage.addChild(three);
    }

    if (wormholes[4] === 1) {
        var holeFiveX = x + (this.width - 79);
        var holeFiveY = y + 57;
        var four = new createjs.Shape();
        four.graphics.f(this.holeColor).s(this.outline);
        four.graphics.arc(holeFiveX, holeFiveY, 7, Math.PI * 1.333, Math.PI * 0.333);
        this.stage.addChild(four);
    }

    if (wormholes[5] === 1) {
        var holeSixX = x + (this.width - 79);
        var holeSixY = y + 19;
        var five = new createjs.Shape();
        five.graphics.f(this.holeColor).s(this.outline);
        five.graphics.arc(holeSixX, holeSixY, 7, Math.PI * 1.666, Math.PI * 0.666);
        this.stage.addChild(five);
    }


};

HexGrid.prototype.drawWormHolesBig = function(x, y, wormholes) {

    var radius = 250;
    var bigRad = 30;
    var height = Math.sqrt(3) * radius;
    var width = 2 * radius;

    if (wormholes[0] === 1) {
        var holeOneX = x + (width - 250);
        var holeOneY = y;
        var zero = new createjs.Shape();
        zero.graphics.f(this.holeColor).s(this.outline);
        zero.graphics.arc(holeOneX, holeOneY, bigRad, 0, Math.PI);
        this.stage.addChild(zero);
    }

    if (wormholes[1] === 1) {
        var holeTwoX = x + (width - 65);
        var holeTwoY = y + 105;
        var one = new createjs.Shape();
        one.graphics.f(this.holeColor).s(this.outline);
        one.graphics.arc(holeTwoX, holeTwoY, bigRad, Math.PI * 0.333, Math.PI * 1.333);
        this.stage.addChild(one);
    }

    if (wormholes[2] === 1) {
        var holeThreeX = x + (width - 62);
        var holeThreeY = y + 323;
        var two = new createjs.Shape();
        two.graphics.f(this.holeColor).s(this.outline);
        two.graphics.arc(holeThreeX, holeThreeY, bigRad, Math.PI * 0.666, Math.PI * 1.666);
        this.stage.addChild(two);
    }

    if (wormholes[3] === 1) {
        var holeFourX = x + (width - 250);
        var holeFourY = y + height;
        var three = new createjs.Shape();
        three.graphics.f(this.holeColor).s(this.outline);
        three.graphics.arc(holeFourX, holeFourY, bigRad, Math.PI, 0);
        this.stage.addChild(three);
    }

    if (wormholes[4] === 1) {
        var holeFiveX = x + 62;
        var holeFiveY = y + 323;
        var four = new createjs.Shape();
        four.graphics.f(this.holeColor).s(this.outline);
        four.graphics.arc(holeFiveX, holeFiveY, bigRad, Math.PI * 1.333, Math.PI * 0.333);
        this.stage.addChild(four);
    }

    if (wormholes[5] === 1) {
        var holeSixX = x + 65;
        var holeSixY = y + 105;
        var five = new createjs.Shape();
        five.graphics.f(this.holeColor).s(this.outline);
        five.graphics.arc(holeSixX, holeSixY, bigRad, Math.PI * 1.666, Math.PI * 0.666);
        this.stage.addChild(five);
    }
};

HexGrid.prototype.drawIncomeBig = function(x, y, income, active, color) {

    var circle = new createjs.Shape();
    circle.graphics.beginStroke(color);
    circle.graphics.setStrokeStyle(1);
    circle.graphics.beginFill(color).drawCircle(0, 0, 10);
    circle.x = x;
    circle.y = y;
    this.stage.addChild(circle);

};

HexGrid.prototype.drawAdvancedIncome = function (x, y, count, active, color) {
    var circle = new createjs.Shape();
    circle.graphics.beginStroke(color);
    circle.graphics.setStrokeStyle(1);
    circle.graphics.beginFill(color).drawCircle(0, 0, 10);
    circle.x = x;
    circle.y = y;
    this.stage.addChild(circle);


    var advance = new createjs.Bitmap(this.advancedIncome);
    advance.x = x - 12;
    advance.y = y - 13 ;
    this.stage.addChild(advance);


    
};

HexGrid.prototype.drawCloseButton = function() {

    var background = new createjs.Shape();
    background.graphics.beginFill("red").drawRoundRect(0, 40, 150, 60, 10);

    var label = new createjs.Text("Close", "bold 24px Sans-Serif", "#FFFFFF");
    label.textAlign = "center";
    label.textBaseline = "middle";
    label.x = 150 / 2;
    label.y = 70;

    var button = new createjs.Container();
    button.x = 20;
    button.y = 20;
    button.addChild(background, label);
    button.mouseChildren = false;
    button.cursor = "pointer";

    button.addEventListener("click", this.drawGameBoard.bind(this));
    this.stage.addChild(button);

};

HexGrid.prototype.takeAction = function(event) {
    var mapId = event.target.name;
    //console.info(mapId);

    switch(mapId) {
    
        case 'EXP':
            console.log('explore');
            this.prepareExplore();
            break;
        case 'INF':
            console.log('Influence');
            break;
        case 'RES':
            console.log('Research');
            break;
        case 'UPG':
            console.log('Upgrade');
            break;
        case 'BUI':
            console.log('Build');
            break;
        case 'MOV':
            console.log('Move');
            break;
        default:
            console.log('dafuk');


    }
    
};

HexGrid.prototype.examineShip = function(event) {
    var shipType = event.target.name;
    console.log(shipType);

    if (shipType < 0) {
        console.log('nothing to see here');
    }

    var idx = -1;
    for (var i = 0; i < this.playerShips.length; i++) {
        console.log(this.playerShips[i].ShipId);
        if (this.playerShips[i].ShipId == shipType) {
            idx = i;
            break;
        }
    }

    if (idx > -1) {

        var spaceShip = this.playerShips[idx];
        
        if (spaceShip.ShipType == "interceptor") {
            this.examineInterceptor(spaceShip);
        } else {
            for (var x = 0; x < spaceShip.Components.length; x++) {
                console.info(spaceShip.Components[x].ComponentName);
            }
        }


    } else {
        console.log('ship not found');
    }
};

HexGrid.prototype.examineInterceptor = function(ship) {

    console.info(ship);

    var infoX = 650;
    var infoY = 250;
    
    var title = new createjs.Text("INTERCEPTOR", "32px Sans-Serif", "#000000");
    title.x = infoX;
    title.y = infoY;
    this.stage.addChild(title);

    var backOne = new createjs.Shape();
    backOne.name = "background";
    backOne.graphics.beginFill("red").drawRoundRect(infoX, infoY + 40, 120, 120, 5);

    var backTwo = new createjs.Shape();
    backTwo.name = "background";
    backTwo.graphics.beginFill("red").drawRoundRect(infoX + 150, infoY + 40, 120, 120, 5);




    var backThree = new createjs.Shape();
    backThree.name = "background";
    backThree.graphics.beginFill("red").drawRoundRect(infoX, infoY + 210, 120, 120, 5);

    var backFour = new createjs.Shape();
    backFour.name = "background";
    backFour.graphics.beginFill("red").drawRoundRect(infoX + 150, infoY + 210, 120, 120, 5);

    this.stage.addChild(backOne, backTwo, backThree, backFour);


    var oneName = (ship.Components[0].ComponentName != null) ? ship.Components[0].ComponentName : "Empty";
    var oneDesc = new createjs.Text(oneName, "bold 14px Sans-Serif", "#000000");
    oneDesc.x = infoX;
    oneDesc.y = infoY + 170;

    var twoName = (ship.Components[1] != null) ? ship.Components[1].ComponentName : "Empty";
    var twoDesc = new createjs.Text(twoName, "bold 14px Sans-Serif", "#000000");
    twoDesc.x = infoX + 150;
    twoDesc.y = infoY + 170;

    var threeName = (ship.Components[2] != null) ? ship.Components[2].ComponentName : "Empty";
    var threeDesc = new createjs.Text(threeName, "bold 14px Sans-Serif", "#000000");
    threeDesc.x = infoX;
    threeDesc.y = infoY + 340;

    var fourName = (ship.Components[3] != null) ? ship.Components[3].ComponentName : "Empty";
    var fourDesc = new createjs.Text(fourName, "bold 14px Sans-Serif", "#000000");
    fourDesc.x = infoX + 150;
    fourDesc.y = infoY + 340;



    var itemOne = new createjs.Bitmap(this.viking);
    itemOne.x = infoX + 10;
    itemOne.y = infoY + 50;

    var itemTwo = new createjs.Bitmap(this.viking);
    itemTwo.x = infoX + 160;
    itemTwo.y = infoY + 50;

    var itemThree = new createjs.Bitmap(this.viking);
    itemThree.x = infoX + 10;
    itemThree.y = infoY + 220;

    this.stage.addChild(itemOne, itemTwo, itemThree, oneDesc, twoDesc, threeDesc, fourDesc);

    var initLbl = new createjs.Text("Initiative : " + ship.Stats.Initiative, "20px Sans-Serif", "#000000");
    initLbl.x = infoX;
    initLbl.y = infoY + 360;

    var computerLbl = new createjs.Text("Computers : " + ship.Stats.Computers, "20px Sans-Serif", "#000000");
    computerLbl.x = infoX;
    computerLbl.y = infoY + 380;

    var driveLbl = new createjs.Text("Drive : " + ship.Stats.Drive, "20px Sans-Serif", "#000000");
    driveLbl.x = infoX;
    driveLbl.y = infoY + 400;

    var hullPointsLbl = new createjs.Text("Hull Points : " + ship.Stats.HullPoints, "20px Sans-Serif", "#000000");
    hullPointsLbl.x = infoX;
    hullPointsLbl.y = infoY + 420;

    var totalPowerLbl = new createjs.Text("Total Power : " + ship.Stats.PowerAvailable, "20px Sans-Serif", "#000000");
    totalPowerLbl.x = infoX;
    totalPowerLbl.y = infoY + 440;

    var powerConsumptionLbl = new createjs.Text("Power Consumed : " + ship.Stats.PowerConsumption, "20px Sans-Serif", "#000000");
    powerConsumptionLbl.x = infoX;
    powerConsumptionLbl.y = infoY + 460;

    var shieldsLbl = new createjs.Text("Shields : " + ship.Stats.Shields, "20px Sans-Serif", "#000000");
    shieldsLbl.x = infoX;
    shieldsLbl.y = infoY + 480;

    var yellowDiceLbl = new createjs.Text("Yellow Dice : " + ship.Stats.YellowDice, "20px Sans-Serif", "#000000");
    yellowDiceLbl.x = infoX;
    yellowDiceLbl.y = infoY + 500;

    var orangeDiceLbl = new createjs.Text("Orange Dice : " + ship.Stats.OrangeDice, "20px Sans-Serif", "#000000");
    orangeDiceLbl.x = infoX;
    orangeDiceLbl.y = infoY + 520;

    var redDiceLbl = new createjs.Text("Red Dice : " + ship.Stats.RedDice, "20px Sans-Serif", "#000000");
    redDiceLbl.x = infoX;
    redDiceLbl.y = infoY + 540;

    this.stage.addChild(initLbl, computerLbl, driveLbl, hullPointsLbl, totalPowerLbl, powerConsumptionLbl, shieldsLbl, yellowDiceLbl, orangeDiceLbl, redDiceLbl);


    this.stage.update();
};

HexGrid.prototype.prepareExplore = function() {
    this.drawGameBoard();
    var context = this;

    $.ajax({ url: "/Turn/GetExploreNeighborsByPlayer", data: { "gameId": this.gameId, "player": this.playerName } }).done(function (data) {
        context.drawExploreTiles(data);
    });
};

HexGrid.prototype.drawExploreTiles = function(tiles) {

    this.exploreTiles = tiles;
    for (var i = 0; i < this.exploreTiles.length; i++) {
        console.log(this.exploreTiles[i].X, this.exploreTiles[i].Y);
        this.drawExploreCandidateTile(this.exploreTiles[i].X, this.exploreTiles[i].Y);
    }

    this.stage.update();

};

HexGrid.prototype.drawExploreCandidateTile = function(xCoord, yCoord) {

    var y0 = xCoord % 2 == 0 ? (yCoord * this.height) + this.canvasOriginY : (yCoord * this.height) + this.canvasOriginY + (this.height / 2);
    var x0 = (xCoord * this.side) + this.canvasOriginX;
    
    var polygon = new createjs.Shape();
    polygon.graphics.beginStroke("#003432").beginFill("DeepSkyBlue");
    polygon.graphics.moveTo(x0 + this.width - this.side, y0)
        .lineTo(x0 + this.side, y0)
        .lineTo(x0 + this.width, y0 + (this.height / 2))
        .lineTo(x0 + this.side, y0 + this.height)
        .lineTo(x0 + this.width - this.side, y0 + this.height)
        .lineTo(x0, y0 + (this.height / 2));
    polygon.graphics.closePath();

    this.stage.addChild(polygon);


    var background = new createjs.Shape();
    background.graphics.beginFill("gray").drawCircle(0, 0, 25);
    background.x = x0 + 25;
    background.y = y0 + 20;

    var label = new createjs.Text("Select", "bold 14px Sans-Serif", "#FFFFFF");
    label.textAlign = "center";
    label.textBaseline = "middle";
    label.x = x0 + 25;
    label.y = y0 + 20;

    var button = new createjs.Container();
    button.x = 20;
    button.y = 20;
    button.addChild(background, label);
    button.mouseChildren = false;
    button.cursor = "pointer";
    button.name = xCoord + "," + yCoord;

    button.addEventListener("click", this.exploreThisTile.bind(this));
    this.stage.addChild(button);


};

HexGrid.prototype.exploreThisTile = function(event) {
    this.drawGameBoard();

    console.log(event.target.name);

    var x, y;
    var coords = event.target.name.split(',');
    x = coords[0];
    y = coords[1];
    console.info(x, y);
    var context = this;

    //int x, int y, string gameId, string player
    $.ajax({ url: '/Turn/ExploreByPlayer', method: "POST", data: { "x" : x, "y": y, "gameId": this.gameId, "player": this.playerName} }).done(function(data) {
        console.info(data);
        context.drawGameTile(data);
    });


};

HexGrid.prototype.setWormholesOnTile = function(tile) {

    this.selectedTile = tile;

    var y0 = tile.x % 2 == 0 ? (tile.y * this.height) + this.canvasOriginY : (tile.y * this.height) + this.canvasOriginY + (this.height / 2);
    var x0 = (tile.x * this.side) + this.canvasOriginX;
    
    //make sure the box isn't covering up the tile
    var rectX = (x0 > 500) ? 25 : 525;
    var rectY = (y0 > 500) ? 25 : 525;



    var background = new createjs.Shape();
    background.graphics.beginStroke("red");
    background.graphics.setStrokeStyle(2);
    background.graphics.beginFill("white").drawRoundRect(rectX, rectY, 450, 150, 10);

    var label = new createjs.Text("Select wormhole orientation", "bold 24px Sans-Serif", "#000000");
    label.x = rectX + 25;
    label.y = rectY + 25;

    var startAngle = 120 * Math.PI / 180;
    var endAngle = 60 * Math.PI / 180;

    var arc = new createjs.Shape();
    arc.graphics.beginStroke("black");
    arc.graphics.setStrokeStyle(4);
    arc.graphics.arc(rectX + 50, rectY + 100, 25, startAngle, endAngle);

    var arrow = new createjs.Shape();
    arrow.graphics.beginStroke("black").setStrokeStyle(4).moveTo(-10, +10).lineTo(0, 0).lineTo(-10, -10);
    var degree = 120 / Math.PI * 180;
    arrow.x = rectX + 41;
    arrow.y = rectY + 124;
    arrow.rotation = degree;

    var counterBack = new createjs.Shape();
    counterBack.graphics.beginStroke("red");
    counterBack.graphics.setStrokeStyle(2);
    counterBack.graphics.beginFill("#FFAAAA").drawRoundRect(rectX + 20, rectY + 70, 60, 60, 10);

    var counter = new createjs.Container();
    counter.x = 0;
    counter.y = 0;
    counter.addChild(counterBack, arc, arrow);
    counter.mouseChildren = false;
    counter.cursor = "pointer";
    counter.name = "counter";
    counter.addEventListener("click", this.rotateWormholes.bind(this));

    var clockArc = new createjs.Shape();
    clockArc.graphics.beginStroke("black");
    clockArc.graphics.setStrokeStyle(4);
    clockArc.graphics.arc(rectX + 150, rectY + 100, 25, startAngle, endAngle);

    var clockArrow = new createjs.Shape();
    clockArrow.graphics.beginStroke("black").setStrokeStyle(4).moveTo(-10, +10).lineTo(0, 0).lineTo(-10, -10);
    clockArrow.x = rectX + 160;
    clockArrow.y = rectY + 124;
    clockArrow.rotation = (400 / Math.PI);

    var clockBack = new createjs.Shape();
    clockBack.graphics.beginStroke("red");
    clockBack.graphics.setStrokeStyle(2);
    clockBack.graphics.beginFill("#FFAAAA").drawRoundRect(rectX + 120, rectY + 70, 60, 60, 10);

    var clockwise = new createjs.Container();
    clockwise.x = 0;
    clockwise.y = 0;
    clockwise.addChild(clockBack, clockArc, clockArrow);
    clockwise.mouseChildren = false;
    clockwise.cursor = "pointer";
    clockwise.name = "clockwise";
    clockwise.addEventListener("click", this.rotateWormholes.bind(this));
    


    var acceptBack = new createjs.Shape();
    acceptBack.graphics.beginStroke("black");
    acceptBack.graphics.setStrokeStyle(2);
    acceptBack.graphics.beginFill("green").drawRoundRect(rectX + 240, rectY + 70, 120, 60, 10);

    var acceptLbl = new createjs.Text("Acceptable", "bold 20px Sans-Serif", "#FFFFFF");
    acceptLbl.textAlign = "center";
    acceptLbl.textBaseline = "middle";
    acceptLbl.x = rectX + 300;
    acceptLbl.y = rectY + 100;

    var accept = new createjs.Container();
    accept.x = 0;
    accept.y = 0;
    accept.addChild(acceptBack, acceptLbl);
    accept.mouseChildren = false;
    accept.cursor = "pointer";
    accept.name = "clockwise";
    accept.addEventListener("click", this.acceptWormholes.bind(this));


    this.stage.addChild(background, label, counter, clockwise, accept);

    
    
    var playerColor = this.getColor(tile.Occupied);

    var polygon = new createjs.Shape();
    polygon.graphics.beginStroke("#003432").beginFill(playerColor);
    polygon.graphics.moveTo(x0 + this.width - this.side, y0)
        .lineTo(x0 + this.side, y0)
        .lineTo(x0 + this.width, y0 + (this.height / 2))
        .lineTo(x0 + this.side, y0 + this.height)
        .lineTo(x0 + this.width - this.side, y0 + this.height)
        .lineTo(x0, y0 + (this.height / 2));
    polygon.graphics.closePath();
    this.stage.addChild(polygon);

    this.drawWormHolesSmall(x0, y0, tile.Wormholes);

    
    this.stage.update();

};

HexGrid.prototype.rotateWormholes = function(event) {

    var direction = event.target.name;
    console.info("turning", direction);
    var holes = this.selectedTile.Wormholes;
    console.log(holes, this.selectedTile.MapId);

    if (direction == 'clockwise') {
        var popped = holes.pop();
        holes.unshift(popped);

        if (this.wormIndex == 0) {
            this.wormIndex = 5;
        } else {
            this.wormIndex--;
        }

    } else {
        var shifted = holes.shift();
        holes.push(shifted);

        if (this.wormIndex == 5) {
            this.wormIndex = 0;
        } else {
            this.wormIndex++;
        }
    }
    this.selectedTile.Wormholes = holes;
    this.setWormholesOnTile(this.selectedTile);

};

HexGrid.prototype.acceptWormholes = function(event) {

    console.log('i deem this acceptable', this.selectedTile.MapId, this.wormIndex);
    var context = this;
    //SetWormholeIndex(int map, int index, string gameId, string name)
    $.ajax({ url: "/Turn/SetWormHoleIndex", method: "POST", data: { "map": this.selectedTile.MapId, "index": this.wormIndex, "gameId": this.gameId, "name": this.playerName } }).done(function() {

        console.log(context.gameTiles);

        for (var i = 0; i < context.gameTiles.length; i++) {
            if (context.selectedTile.MapId == context.gameTiles[i].MapId) {
                context.gameTiles[i].Wormholes = context.selectedTile.Wormholes;
                context.drawGameBoard();
            }
        }

        
    });


};