# Petite Ville

### Made by

<div>
    <link rel="stylsheet" href="https://raw.githubusercontent.com/myconstructivismmess/css-readme-code-samples/main/contributor-card/style.css" />
    <div class="contributor-card-container">
        <a href="https://github.com/TNtube" target="_blank" class="contributor-card">
            <img
                class="contributor-card-avatar"
                src="https://avatars.githubusercontent.com/u/51389578"
                alt="KAPUSTA Stanis Avatar Image"
            />
            <div class="contributor-card-text-container">
                <h3 class="contributor-card-name">KAPUSTA Stanis</h3>
                <p class="contributor-card-role">Lead Developer</p>
            </div>
        </a>
        <a href="https://github.com/myconstructivismmess" target="_blank" class="contributor-card">
            <img
                class="contributor-card-avatar"
                src="https://avatars.githubusercontent.com/u/39066566"
                alt="ROUET Aubrey Avatar Image"
            />
            <div class="contributor-card-text-container">
                <h3 class="contributor-card-name">ROUET Aubrey</h3>
                <p class="contributor-card-role">GUI Developer</p>
            </div>
        </a>
        <a href="https://github.com/maYayoh" target="_blank" class="contributor-card">
            <img
                class="contributor-card-avatar"
                src="https://avatars.githubusercontent.com/u/93120487"
                alt="COURTAND Yohann Avatar Image"
            />
            <div class="contributor-card-text-container">
                <h3 class="contributor-card-name">COURTAND Yohann</h3>
                <p class="contributor-card-role">Developer</p>
            </div>
        </a>
        <a href="https://github.com/Ch0pper26" target="_blank" class="contributor-card">
            <img
                class="contributor-card-avatar"
                src="https://avatars.githubusercontent.com/u/91217238"
                alt="BLONDEEL Eliot Avatar Image"
            />
            <div class="contributor-card-text-container">
                <h3 class="contributor-card-name">BLONDEEL Eliot</h3>
                <p class="contributor-card-role">Developer</p>
            </div>
        </a>
    </div>
</div>

### With

<div>
    <link rel="stylsheet" href="https://raw.githubusercontent.com/myconstructivismmess/css-readme-code-samples/main/contributor-card/style.css" />
    <div class="contributor-card-container">
        <a href="https://github.com/MonoGame/MonoGame" target="_blank" class="contributor-card">
            <img
                class="contributor-card-avatar"
                src="https://avatars.githubusercontent.com/u/4772066"
                alt="MonoGame Avatar Image"
            />
            <div class="contributor-card-text-container">
                <h3 class="contributor-card-name">MonoGame</h3>
                <p class="contributor-card-role">2D/3D Rendering Framework</p>
            </div>
        </a>
    </div>
</div>

### With the help of :

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
