# Miniville

Produit par : 
- Tom Rouet
- Eliot Blondeel
- Yohann Courtand
- Stanis Kapusta


Miniville est un jeu de plateau en tour par tour où le but est de construire \ 
sa propre ville en achetant des propriétés et en devenant riche !

Le déroulé d'un tour est le suivant :
- Le joueur lance un dés (ou plusieurs, si ses propriétés le lui permet)
- Chaque carte du jeu (les propriétés) on une valeur d'activation. Si la valeur des \
dés est la bonne, alors le joueur gagne le nombre de pièces associer.
- Il a la possibilité de gagner des monuments qui lui permet de faire des actions spéciales.
- Le tour se fini si le joueur achète une nouvelle carte ou décide d'économiser.

La partie se fini, dans notre implémentation du jeu, quand l'un des joueurs a construit \
les 4 monuments.

Vous avez la possibilité de jouer à 2 versions du jeu. Une en console, l'autre avec une \
interface graphique, Monogame.

Si vous souhaitez compiler vous même le code, vous devrez installer au préalable le package \
Monogame sur NuGet.

Le code est constitué d'une solution, elle même divisée en 3 projets :
- `Core`, étant le coeur même du jeu et contenant toute les logiques nécessairent à la création \
d'une partie.
- `MinivilleConsole`, qui est l'implémentation en TUI (Terminal User Interface) du jeu.
- `MinivilleGUI`, qui est l'implémentation en GUI (Graphical User Interface) du jeu.
