// Hex math defined here: http://blog.ruslans.com/2011/02/hexagonal-grid-math.html

//  Repo - https://github.com/rrreese/Hexagon.js

function HexagonGrid(canvasId, radius, background, orange, brown, pink, cols, rows, explore) {
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

    this.setNewHexDimensions();

    this.canvas = document.getElementById(canvasId);
    this.context = this.canvas.getContext('2d');

    this.canvasOriginX = 0;
    this.canvasOriginY = 0;

    this.canvas.addEventListener("mousedown", this.clickEvent.bind(this), false);
};

HexagonGrid.prototype.setNewHexDimensions = function () {
    this.height = Math.sqrt(3) * this.radius;
    this.width = 2 * this.radius;
    this.side = (3 / 2) * this.radius;
};

HexagonGrid.prototype.drawHexGrid = function (originX, originY, explore) {
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


HexagonGrid.prototype.drawScaledGrid = function (hexes) {

    var rows = [];
    var cols = [];

    var currentHexX;
    var currentHexY;
    var debugText = "";
    var offsetColumn = false;

    for (var i = 0; i < hexes.length; i++) {
        if (rows.indexOf(hexes[i].x) == -1) {
            rows.push(hexes[i].x);
        }

        if (cols.indexOf(hexes[i].y) == -1) {
            cols.push(hexes[i].y);
        }
    }

    rows.sort();
    cols.sort();

    //figure out radius
    var canvasWidth = 1000;

    console.info('before : ' + this.radius);
    this.radius = (canvasWidth / (cols.length)) / 2;
    console.info('after : ' + this.radius);
    this.setNewHexDimensions();

    this.canvasOriginX = this.radius;
    this.canvasOriginY = this.radius;
    var originX = this.radius;
    var originY = this.radius;


    for (var col = 0; col < cols.length; col++) {
        for (var row = 0; row < rows.length; row++) {

            if (!offsetColumn) {
                currentHexX = (col * this.side) + originX;
                currentHexY = (row * this.height) + originY;
            } else {
                currentHexX = col * this.side + originX;
                currentHexY = (row * this.height) + originY + (this.height * 0.5);
            }

            debugText = cols[col] + "," + rows[row];

            this.drawHex(currentHexX, currentHexY, this.softWhite, debugText);
        }
        offsetColumn = !offsetColumn;
    }



};


HexagonGrid.prototype.drawHexAtColRow = function (column, row, color) {
    this.redrawHexAtColRow(column, row, color, "");
};

HexagonGrid.prototype.redrawHexAtColRow = function (column, row, color, text) {
    var drawy = column % 2 == 0 ? (row * this.height) + this.canvasOriginY : (row * this.height) + this.canvasOriginY + (this.height / 2);
    var drawx = (column * this.side) + this.canvasOriginX;
    this.removeHex(drawx, drawy);
    this.drawHex(drawx, drawy, color, text);
};

HexagonGrid.prototype.removeHex = function (x0, y0) {
    this.context.strokeStyle = this.background;
    this.context.beginPath();
    this.context.moveTo(x0 + this.width - this.side, y0);
    this.context.lineTo(x0 + this.side, y0);
    this.context.lineTo(x0 + this.width, y0 + (this.height / 2));
    this.context.lineTo(x0 + this.side, y0 + this.height);
    this.context.lineTo(x0 + this.width - this.side, y0 + this.height);
    this.context.lineTo(x0, y0 + (this.height / 2));
    this.context.fillStyle = this.background;
    this.context.fill();


    this.context.closePath();
    this.context.stroke();


};

HexagonGrid.prototype.drawHex = function (x0, y0, fillColor, debugText) {
    this.context.strokeStyle = "#003432";
    this.context.setLineDash([5, 2]);
    this.context.beginPath();
    this.context.moveTo(x0 + this.width - this.side, y0);
    this.context.lineTo(x0 + this.side, y0);
    this.context.lineTo(x0 + this.width, y0 + (this.height / 2));
    this.context.lineTo(x0 + this.side, y0 + this.height);
    this.context.lineTo(x0 + this.width - this.side, y0 + this.height);
    this.context.lineTo(x0, y0 + (this.height / 2));

    if (fillColor) {
        this.context.fillStyle = fillColor;
        this.context.fill();
    }

    this.context.closePath();
    this.context.stroke();

    if (this.explore) {
        this.context.font = "Bold 36px Sans-Serif";
        this.context.fillStyle = "rgb(158,101,101,0.5)";
        this.context.fillText(debugText, x0 + (this.width / 2) - 10, y0 + (this.height - 25));
    } else {
        this.context.font = "10px sans serif";
        this.context.fillStyle = "#000";
        this.context.fillText(debugText, x0 + (this.width / 2) - (this.width / 4), y0 + (this.height - 5));
    }
};

//Recusivly step up to the body to calculate canvas offset.
HexagonGrid.prototype.getRelativeCanvasOffset = function () {
    var x = 0, y = 0;
    var layoutElement = this.canvas;
    if (layoutElement.offsetParent) {
        do {
            x += layoutElement.offsetLeft;
            y += layoutElement.offsetTop;
        } while (layoutElement = layoutElement.offsetParent);

        return { x: x, y: y };
    }
}

//Uses a grid overlay algorithm to determine hexagon location
//Left edge of grid has a test to acuratly determin correct hex
HexagonGrid.prototype.getSelectedTile = function (mouseX, mouseY) {

    var offSet = this.getRelativeCanvasOffset();

    mouseX -= offSet.x;
    mouseY -= offSet.y;

    var column = Math.floor((mouseX) / this.side);
    var row = Math.floor(
        column % 2 == 0
            ? Math.floor((mouseY) / this.height)
            : Math.floor(((mouseY + (this.height * 0.5)) / this.height)) - 1);


    //Test if on left side of frame            
    if (mouseX > (column * this.side) && mouseX < (column * this.side) + this.width - this.side) {


        //Now test which of the two triangles we are in 
        //Top left triangle points
        var p1 = new Object();
        p1.x = column * this.side;
        p1.y = column % 2 == 0
            ? row * this.height
            : (row * this.height) + (this.height / 2);

        var p2 = new Object();
        p2.x = p1.x;
        p2.y = p1.y + (this.height / 2);

        var p3 = new Object();
        p3.x = p1.x + this.width - this.side;
        p3.y = p1.y;

        var mousePoint = new Object();
        mousePoint.x = mouseX;
        mousePoint.y = mouseY;

        if (this.isPointInTriangle(mousePoint, p1, p2, p3)) {
            column--;

            if (column % 2 != 0) {
                row--;
            }
        }

        //Bottom left triangle points
        var p4 = new Object();
        p4 = p2;

        var p5 = new Object();
        p5.x = p4.x;
        p5.y = p4.y + (this.height / 2);

        var p6 = new Object();
        p6.x = p5.x + (this.width - this.side);
        p6.y = p5.y;

        if (this.isPointInTriangle(mousePoint, p4, p5, p6)) {
            column--;

            if (column % 2 == 0) {
                row++;
            }
        }
    }

    return { row: row, column: column };
};


HexagonGrid.prototype.sign = function (p1, p2, p3) {
    return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
};

//TODO: Replace with optimized barycentric coordinate method
HexagonGrid.prototype.isPointInTriangle = function isPointInTriangle(pt, v1, v2, v3) {
    var b1, b2, b3;

    b1 = this.sign(pt, v1, v2) < 0.0;
    b2 = this.sign(pt, v2, v3) < 0.0;
    b3 = this.sign(pt, v3, v1) < 0.0;

    return ((b1 == b2) && (b2 == b3));
};

HexagonGrid.prototype.clickEvent = function (e) {
    if (!this.bigHexUp) {
        var mouseX = e.pageX;
        var mouseY = e.pageY;

        var localX = mouseX - this.canvasOriginX;
        var localY = mouseY - this.canvasOriginY;

        var tile = this.getSelectedTile(localX, localY);
        if (tile.column >= 0 && tile.row >= 0) {

            console.info("column : " + tile.column + " :: row : " + tile.row);

            var drawy = tile.column % 2 == 0 ? (tile.row * this.height) + this.canvasOriginY + 6 : (tile.row * this.height) + this.canvasOriginY + 6 + (this.height / 2);
            var drawx = (tile.column * this.side) + this.canvasOriginX;

            //console.info("x : " + drawx + " :: y : " + drawy);

            this.drawHex(drawx, drawy - 6, "rgba(110,110,70,0.3)", "");
            this.bigHexUp = true;
            this.drawBigHex(tile.column, tile.row);

            if (this.exploreMode) {
                this.makeExploreCall(tile.column, tile.row, 2);
            }
        }
    } else {

        console.info('exploring: ', this.explore);

        if (this.explore) {
            console.info('exploration continues!');

            //http://stackoverflow.com/questions/18758997/call-angular-function-with-jquery

            //$scope.tileCounts.DivisionOne = 100;
        }

        console.log('Big hex up');
        this.bigHexUp = false;
        this.buildGameHexes();


    }
    
};


var GameTile = function(x, y, color, victoryPoints) {
    this.x = x;
    this.y = y;
    this.color = color;
    this.victoryPoints = victoryPoints;
};


HexagonGrid.prototype.clickyEvent = function (e) {
    var mouseX = e.pageX;
    var mouseY = e.pageY;

    var localX = mouseX - this.canvasOriginX;
    var localY = mouseY - this.canvasOriginY;

    var tile = this.getSelectedTile(localX, localY);
    if (tile.column >= 0 && tile.row >= 0) {

        console.info("column : " + tile.column + " :: row : " + tile.row);

        var drawy = tile.column % 2 == 0 ? (tile.row * this.height) + this.canvasOriginY + 6 : (tile.row * this.height) + this.canvasOriginY + 6 + (this.height / 2);
        var drawx = (tile.column * this.side) + this.canvasOriginX;

        console.info("x : " + drawx + " :: y : " + drawy);

        //this.drawHex(drawx, drawy - 6, "rgba(110,110,70,0.3)", "");
    }
};

HexagonGrid.prototype.buildCentralHex = function(tile) {
    this.redrawHexAtColRow(tile.x, tile.y, this.getColor(tile.Occupied));
};

HexagonGrid.prototype.buildGameHex = function(tile) {
    var drawy = tile.x % 2 == 0 ? (tile.y * this.height) + this.canvasOriginY : (tile.y * this.height) + this.canvasOriginY + (this.height / 2);
    var drawx = (tile.x * this.side) + this.canvasOriginX;
    this.removeHex(drawx, drawy);
    //this.drawHex(drawx, drawy, tile.color, tile.victoryPoints);

    if (tile.Occupied === 'Central') {
        this.buildCentralHex(tile);
        return false;
    } 
    
    var playerColor = this.getColor(tile.Occupied);

    this.context.strokeStyle = "#003432";
    this.context.setLineDash([5, 2]);
    this.context.beginPath();
    this.context.moveTo(drawx + this.width - this.side, drawy);
    this.context.lineTo(drawx + this.side, drawy);
    this.context.lineTo(drawx + this.width, drawy + (this.height / 2));
    this.context.lineTo(drawx + this.side, drawy + this.height);
    this.context.lineTo(drawx + this.width - this.side, drawy + this.height);
    this.context.lineTo(drawx, drawy + (this.height / 2));

    this.context.fillStyle = playerColor;
    this.context.fill();

    this.context.closePath();
    this.context.stroke();

    if (tile.VictoryPoints) {
        this.context.font = "20px Sans-Serif";
        this.context.fillStyle = "#FFD300";
        this.context.fillText(tile.VictoryPoints,
            drawx + (this.width - 20),
            drawy + ((this.height / 2) + 5));

    }


    //orange circle
    if (tile.Orange > 0) {
        this.drawOrangeSmall(drawx, drawy);
    }

    //brown circle
    if (tile.Brown > 0) {
        this.drawBrownSmall(drawx, drawy);
    }

    //pink circle
    if (tile.Pink > 0) {
        this.drawPinkSmall(drawx, drawy);
    }

    if (tile.White > 0) {
        this.drawWhiteSmall(drawx, drawy);
    }

    //ALIENS
    if (tile.Aliens > 0) {
        var alienX = drawx + (this.width - 75);
        var alienY = drawy + ((this.height / 2) -10 );
        
        if (tile.Aliens == 2) {

            alienY -= 8;

            var img1 = new Image();
            img1.src = '../Content/Images/alienHeadx25.png';
            var cont1 = this.context;
            cont1.globalAlpha = 1;
            img1.onload = function() {
                cont1.drawImage(img1, alienX, alienY - 7);
            };

            var img2 = new Image();
            img2.src = '../Content/Images/alienHeadx25.png';
            img2.onload = function() {
                cont1.drawImage(img2, alienX, alienY + 17);
            };
        } else {
            var img = new Image();
            img.src = '../Content/Images/alienHeadx25.png';
            var contUno = this.context;
            contUno.globalAlpha = 1;
            img.onload = function () {
                contUno.drawImage(img, alienX, alienY);
            };
        }
    }

    //SHIPS

    var containsShips = false;
    for (var s = 0; s < this.playerShips.length; s++) {
        //console.info('tile:' + tile.x + ',' + tile.y + ' :: ship: ' + this.playerShips[s].XCoords);
        if (tile.x == this.playerShips[s].XCoords && tile.y == this.playerShips[s].YCoords) {
            containsShips = true;
            break;
        }
    }

    if (containsShips) {

        if (tile.Aliens == 0) {
            var shipX = drawx + (this.width - 75);
            var shipY = drawy + ((this.height / 2) - 5);

            var ships = new Image();
            ships.src = (tile.Occupied == 'Black') ? '../Content/Images/rocket-white-x25.png' : '../Content/Images/rocketx25.png';
            //ships.src = '../Content/Images/rocketx25.png';
            var cont = this.context;
            ships.onload = function () {
                cont.drawImage(ships, shipX, shipY - 12);
            };
        }

        
    }

    
    //Player
    //if (tile.Occupied != 'Aliens') {
    //    var playerX = drawx + (this.width - 65);
    //    var playerY = drawy + ((this.height / 2) + 5);
    //    this.context.strokeStyle = playerColor;
    //    this.context.beginPath();
    //    this.context.arc(playerX, playerY, 13, 0, Math.PI * 2, false);
    //    this.context.lineTo(alienX - 6, alienY);
    //    //this.context.lineTo(31, 37);
    //    this.context.fillStyle = playerColor;
    //    this.context.fill();
        
    //}
    
    //Wormholes
    this.drawWormHolesSmall(drawx, drawy, tile.Wormholes);


};

HexagonGrid.prototype.drawBigHex = function(column, row) {
    var x0 = 250;
    var y0 = 250;
    var radius = 250;

    var fillColor = "#003432";

    var height = Math.sqrt(3) * radius;
    var width = 2 * radius;
    var side = (3 / 2) * radius;


    this.context.globalAlpha = 1;
    //this.context.strokeStyle = "#003432";
    this.context.strokeStyle = "#000000";
    //this.context.setLineDash([5, 2]);
    this.context.beginPath();
    this.context.moveTo(x0 + width - side, y0);
    this.context.lineTo(x0 + side, y0);
    this.context.lineTo(x0 + width, y0 + (height / 2));
    this.context.lineTo(x0 + side, y0 + height);
    this.context.lineTo(x0 + width - side, y0 + height);
    this.context.lineTo(x0, y0 + (height / 2));

    this.context.fillStyle = fillColor;
    this.context.fill();
    this.context.closePath();
    this.context.stroke();


    //now get the tile attributes
    var tiles = this.gameTiles;
    //console.info(this.gameTiles);
    var selectedTile;
    for (var i = 0; i < tiles.length; i++) {
        if (tiles[i].y == row && tiles[i].x == column) {
            selectedTile = tiles[i];
            break;
        }
    }

    if (selectedTile == undefined) {
        this.context.font = "62px Sans-Serif";
        this.context.fillStyle = "#FFD300";
        this.context.fillText("Not discovered", x0 + 50, y0 + ((height / 2)) + 25);
        
    } else {
        //console.log("selected", selectedTile);
        
        if (selectedTile.VictoryPoints) {

            this.context.font = "Bold 72px Sans-Serif";
            this.context.setLineDash([0]);
            this.context.strokeStyle = "#FFD300";
            this.context.strokeText(selectedTile.VictoryPoints,
                x0 + 420,
                y0 + ((height / 2)) + 25);

            //this.context.fillStyle = "#FFD300";
            //this.context.fillText(selectedTile.VictoryPoints,
            //    x0 + 420,
            //    y0 + ((height / 2)) + 25);


        } 

        if (selectedTile.Division) {
            this.context.font = "32px Sans-Serif";
            this.context.fillStyle = "#FFD300";
            this.context.fillText("Division " + selectedTile.Division, x0 + 50, y0 + (height / 2) + 30);
        }

        //Incomes

        var incomeX = x0 + 135;
        var incomeY = y0;

        if (selectedTile.Pink > 0) {
            incomeY += 25;
            this.drawIncomeBig(incomeX, incomeY, selectedTile.Pink, 0, this.pink);
        }

        if (selectedTile.Orange > 0) {
            incomeY += 25;
            this.drawIncomeBig(incomeX, incomeY, selectedTile.Orange, 0, this.orange);
        }

        if (selectedTile.Brown > 0) {
            incomeY += 25;
            this.drawIncomeBig(incomeX, incomeY, selectedTile.Brown, 0, this.brown);
        }

        if (selectedTile.White > 0) {
            incomeY += 25;
            this.drawIncomeBig(incomeX, incomeY, selectedTile.White, 0, this.white);
        }


        //reset coordinates
        var adIncomeX = x0 + 175;
        var adIncomeY = y0;

        if (selectedTile.PinkAdvanced > 0) {
            adIncomeY += 25;
            this.drawAdvancedIncome(adIncomeX, adIncomeY, 1, 0, this.pink);
        }

        
        if (selectedTile.OrangeAdvanced > 0) {
            adIncomeY += 25;
            this.drawAdvancedIncome(adIncomeX, adIncomeY, 1, 0, this.orange);
        }

        if (selectedTile.BrownAdvanced > 0) {
            adIncomeY += 25;
            this.drawAdvancedIncome(adIncomeX, adIncomeY, 1, 0, this.brown);
        }

        
        if (selectedTile.Aliens > 0) {
            
            var img = new Image();
            img.src = '../Content/Images/alienHeadx100.png';
            var contAliens = this.context;
            img.onload = function () {
                contAliens.globalAlpha = 1;
                contAliens.drawImage(img, x0 + 100, y0 + 150);
            };

        }

        
        if (selectedTile.Occupied != null) {
            var color = this.getColor(selectedTile.Occupied);
            console.log(color);
            this.drawDiscColorBig(color, x0, y0);
        }

        //wormholes
        this.drawWormHolesBig(x0, y0, selectedTile.Wormholes);


        //SHIPS
        var shipCount = 0;
        var hexShips = { Interceptor : [], Cruiser : [], Dreadnought: []};
        for (var s = 0; s < this.playerShips.length; s++) {
            //console.info(s + ' => tile:' + column + ',' + row + ' :: ship: ' + this.playerShips[s].XCoords + ',' +this.playerShips[s].YCoords);
            if (column == this.playerShips[s].XCoords && row == this.playerShips[s].YCoords) {
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

            //console.info('tile:' + tile.x + ',' + tile.y + ' :: ship: ' + this.playerShips[s].XCoords);
            var shipCon = this.context;
            shipCon.globalAlpha = 1;
            shipCon.font = "32px Sans-Serif";
            shipCon.fillStyle = "#FFD300";
            shipCon.fillText("Ships ", x0 + 120, y0 + (height / 2) + 100);

            shipCon.font = "20px Sans-Serif";
            var shipStartY = y0 + (height / 2) + 130;
            var shipStartX = x0 + 120;
            shipCon.fillText("Interceptors : " + hexShips.Interceptor.length, shipStartX, shipStartY);
            shipCon.fillText("Cruisers : " + hexShips.Cruiser.length, shipStartX, shipStartY + 20);
            shipCon.fillText("Dreadnoughts : " + hexShips.Dreadnought.length, shipStartX, shipStartY + 40);

        }

        

    }

};

HexagonGrid.prototype.setGameHexes = function(tiles) {
    this.gameTiles = tiles;
    this.buildGameHexes();
};

HexagonGrid.prototype.buildGameHexes = function () {

    this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);

    this.drawHexGrid(this.radius, this.radius, this.explore);

    //console.log("building hexes :: count => ", this.gameTiles.length);
    for (var i = 0; i < this.gameTiles.length; i++) {
        this.buildGameHex(this.gameTiles[i]);
    }
};

HexagonGrid.prototype.getColor = function(disc) {
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

HexagonGrid.prototype.whatDivAmI = function(x, y) {

    if (x < 4 || x > 8 || y < 3 || y > 7) {
        return 3;
    }

    
    switch(x) {
    
        case 4:
        case 8:
            if (y < 4 || y > 6) {
                return 3;
            } else {
                return 2;
            }
            break;
        case 5:
        case 7:
            if (y < 3 || y > 6) {
                return 3;
            } else if (y == 3 || y == 6) {
                return 2;
            } else {
                return 1;
            }
            break;

        case 6:
            if (y < 3 || y > 7) {
                return 3;
            } else if (y == 3 || y == 7) {
                return 2;
            } else if (y === 4 || y === 6) {
                return 1;
            } else {
                return 0;
            }
            break;
        default:
            console.warn('div not found');
    }




    return null;


    
};

HexagonGrid.prototype.drawWormHolesSmall = function(x, y, wormholes) {
    
    var wormHoleLine = this.outline;

    // hole[0]
    if (wormholes[0] === 1) {
        var holeOneX = x + (this.width - 45);
        var holeOneY = y;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeOneX, holeOneY, 7, 0, Math.PI, false);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[1]
    if (wormholes[1] === 1) {
        var holeTwoX = x + (this.width - 12);
        var holeTwoY = y + 18;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeTwoX, holeTwoY, 7, Math.PI * 1.333, Math.PI * 0.333, true);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[2]
    if (wormholes[2] === 1) {
        var holeThreeX = x + (this.width - 12);
        var holeThreeY = y + 58;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeThreeX, holeThreeY, 7, Math.PI * 1.666, Math.PI * 0.666, true);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[3]
    if (wormholes[3] === 1) {
        var holeFourX = x + (this.width - 45);
        var holeFourY = y + 78;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeFourX, holeFourY, 7, Math.PI, 0, false);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[4]
    if (wormholes[4] === 1) {
        var holeFiveX = x + (this.width - 79);
        var holeFiveY = y + 57;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeFiveX, holeFiveY, 7, Math.PI * 1.333, Math.PI * 0.333, false);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[5]
    if (wormholes[5] === 1) {
        var holeSixX = x + (this.width - 79);
        var holeSixY = y + 19;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeSixX, holeSixY, 7, Math.PI * 0.666, Math.PI * 1.666, true);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }
};

HexagonGrid.prototype.drawWormHolesBig = function (x, y, wormholes) {

    var wormHoleLine = this.outline;

    var radius = 250;

    var bigRad = 30;

    this.context.globalAlpha = 1;

    var fillColor = "#003432";
    
    var height = Math.sqrt(3) * radius;
    var width = 2 * radius;
    var side = (3 / 2) * radius;

    // hole[0]
    if (wormholes[0] === 1) {
        var holeOneX = x + (width - 250);
        var holeOneY = y;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeOneX, holeOneY, bigRad, 0, Math.PI, false);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();

        
    }

    // hole[1]
    if (wormholes[1] === 1) {
        var holeTwoX = x + (width - 65);
        var holeTwoY = y + 105;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeTwoX, holeTwoY, bigRad, Math.PI * 1.333, Math.PI * 0.333, true);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[2]
    if (wormholes[2] === 1) {
        var holeThreeX = x + (width - 62);
        var holeThreeY = y + 323;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeThreeX, holeThreeY, bigRad, Math.PI * 1.666, Math.PI * 0.666, true);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[3]
    if (wormholes[3] === 1) {
        var holeFourX = x + (width - 250);
        var holeFourY = y + height;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeFourX, holeFourY, bigRad, Math.PI, 0, false);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[4]
    if (wormholes[4] === 1) {
        var holeFiveX = x + 62;
        var holeFiveY = y + 323;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeFiveX, holeFiveY, bigRad, Math.PI * 1.333, Math.PI * 0.333, false);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }

    // hole[5]
    if (wormholes[5] === 1) {
        var holeSixX = x + 65;
        var holeSixY = y + 105;
        this.context.strokeStyle = wormHoleLine;
        this.context.beginPath();
        this.context.arc(holeSixX, holeSixY, bigRad, Math.PI * 0.666, Math.PI * 1.666, true);
        this.context.fillStyle = this.holeColor;
        this.context.fill();
        this.context.stroke();
    }


    this.context.globalAlpha = 0.7;

};

HexagonGrid.prototype.drawOrangeSmall = function(x, y) {
    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = this.outline;
    this.context.setLineDash([]);
    var orangeX = x + (this.width - 40);
    var orangeY = y + ((this.height / 2) + 10);
    this.context.arc(orangeX, orangeY, 5, 0, Math.PI * 2, false);

    this.context.fillStyle = this.orange;
    this.context.fill();

    this.context.stroke();
};

HexagonGrid.prototype.drawPinkSmall = function (x, y) {
    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = this.outline;
    this.context.setLineDash([]);
    var pinkX = x + (this.width - 40);
    var pinkY = y + ((this.height / 2) - 20);
    this.context.arc(pinkX, pinkY, 5, 0, Math.PI * 2, false);
    this.context.fillStyle = this.pink;
    this.context.fill();
    this.context.stroke();
};

HexagonGrid.prototype.drawBrownSmall = function (x, y) {
    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = this.outline;
    this.context.setLineDash([]);
    var brownX = x + (this.width - 40);
    var brownY = y + ((this.height / 2) - 5);
    this.context.arc(brownX, brownY, 5, 0, Math.PI * 2, false);
    this.context.fillStyle = this.brown;
    this.context.fill();
    this.context.stroke();
};

HexagonGrid.prototype.drawWhiteSmall = function (x, y) {
    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = this.outline;
    this.context.setLineDash([]);
    var brownX = x + (this.width - 40);
    var brownY = y + ((this.height / 2) + 25);
    this.context.arc(brownX, brownY, 5, 0, Math.PI * 2, false);
    this.context.fillStyle = this.holeColor;
    this.context.fill();
    this.context.stroke();
};

HexagonGrid.prototype.processShips = function(ships) {
    console.log('SHIPS!');
    this.playerShips = ships;
    console.info(this.playerShips.length);
};

HexagonGrid.prototype.drawDiscColorBig = function(color, x, y) {

    var discX = x + 350;
    var discY = y + 50;

    this.context.globalAlpha = 1;
    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = this.outline;
    this.context.setLineDash([]);
    
    this.context.arc(discX, discY, 30, 0, Math.PI * 2, false);

    this.context.fillStyle = color;
    this.context.fill();

    this.context.stroke();
};

HexagonGrid.prototype.drawIncomeBig = function(x, y, count, active, color) {

    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = color;
    this.context.setLineDash([]);

    this.context.arc(x, y, 10, 0, Math.PI * 2, false);
    this.context.fillStyle = color;
    this.context.fill();
    this.context.stroke();

    this.context.font = "20px Open Sans";
    this.context.fillText(" " + count, x + 10, y + 5);
};

HexagonGrid.prototype.drawAdvancedIncome = function(x, y, count, active, color) {
    this.context.beginPath();
    this.context.restore();
    this.context.strokeStyle = color;
    this.context.setLineDash([]);

    this.context.arc(x, y, 10, 0, Math.PI * 2, false);
    this.context.fillStyle = color;
    this.context.fill();
    this.context.stroke();


    var img = new Image();
    img.src = '../Content/Images/AdvancedStar.png';
    var cont1 = this.context;

    img.onload = function () {
        //cont1.globalAlpha = 0.7;
        cont1.drawImage(img, x - 10, y - 11, 20, 20);
    };

};

HexagonGrid.prototype.toggleExploreMode = function(setting) {
    this.exploreMode = setting;
};

HexagonGrid.prototype.makeExploreCall = function(x, y, div) {
    //$('#messaging').text('Are you sure?');
    $('#messaging').html('Are you sure?  <button class="btn btn-success exploreConfirm">Yep</button>');
};