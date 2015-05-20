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

    //Player Colors
    this.greenPlayer = "#2C8437";
    this.blackPlayer = "#010101";
    this.bluePlayer = "rgba(2,105,255,0.5)";
    this.redPlayer = "#D10F40";
    this.yellowPlayer = "rgba(255,215,0,0.3)";
    this.whitePlayer = "#C4C2B6";
    this.central = "rgba(215,40,40,0.8)";
    this.holeColor = "#D4D4D4";
    this.outline = "#000000";
    this.background = "rgba(225,93,15,0.2)";

    //images 
    this.alienHead = new Image();
    this.alienHead.src = '../Content/Images/alienHeadx25.png';

    this.setNewHexDimensions();

    this.stage = stage;

    this.canvasOriginX = 0;
    this.canvasOriginY = 0;

    this.drawHexGrid(this.radius, this.radius, this.explore);

}

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

HexGrid.prototype.buildGameHexes = function(tiles) {
    for (var i = 0; i < tiles.length; i++) {
        this.drawGameTile(tiles[i]);
    }
    this.stage.update();
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
    polygon.graphics.beginStroke("#003432").beginFill(playerColor);
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


    //ALIENS
    //if (tile.Aliens > 0) {
    //    var alienX = x0 + (this.width - 75);
    //    var alienY = y0 + ((this.height / 2) - 10);

    //    if (tile.Aliens == 2) {

    //        alienY -= 8;

    //        var img1 = new Image();
    //        img1.src = '../Content/Images/alienHeadx25.png';
    //        var cont1 = this.context;
    //        cont1.globalAlpha = 1;
    //        img1.onload = function () {
    //            cont1.drawImage(img1, alienX, alienY - 7);
    //        };

    //        var img2 = new Image();
    //        img2.src = '../Content/Images/alienHeadx25.png';
    //        img2.onload = function () {
    //            cont1.drawImage(img2, alienX, alienY + 17);
    //        };
    //    } else {
    //        var img = new Image();
    //        img.src = '../Content/Images/alienHeadx25.png';
    //        var contUno = this.context;
    //        contUno.globalAlpha = 1;
    //        img.onload = function () {
    //            contUno.drawImage(img, alienX, alienY);
    //        };
    //    }
    //}
    

    //click event
    polygon.on("click", function () { console.info(tile.MapId); });
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

