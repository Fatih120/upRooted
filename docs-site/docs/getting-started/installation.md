# Installation

## Windows

=== "Installer (recommended)"

    Download the latest installer from [GitHub Releases](https://github.com/The-Uprooted-Project/uprooted/releases/latest).

=== "PowerShell one-liner"

    ```powershell
    iex (irm https://raw.githubusercontent.com/The-Uprooted-Project/uprooted/main/Install-Uprooted.ps1)
    ```

=== "Scoop"

    ```powershell
    scoop bucket add uprooted https://github.com/The-Uprooted-Project/scoop-bucket
    scoop install uprooted
    ```

=== "Chocolatey"

    ```powershell
    choco install uprooted
    ```

=== "winget"

    ```powershell
    winget install TheUprootedProject.Uprooted
    ```

## Linux

=== "Installer (recommended)"

    Download the latest installer from [GitHub Releases](https://github.com/The-Uprooted-Project/uprooted/releases/latest).

=== "curl one-liner"

    ```bash
    curl -sSL https://raw.githubusercontent.com/The-Uprooted-Project/uprooted/main/install-uprooted-linux.sh | bash
    ```

=== "APT (Debian/Ubuntu)"

    ```bash
    curl -1sLf 'https://dl.cloudsmith.io/public/the-uprooted-project/uprooted/setup.deb.sh' | sudo bash
    sudo apt install uprooted
    ```

=== "AUR (Arch Linux)"

    ```bash
    yay -S uprooted-bin
    ```

## macOS (experimental)

=== "Homebrew"

    ```bash
    brew install The-Uprooted-Project/tap/uprooted
    ```

!!! note
    Root Communications does not currently ship on macOS. The macOS installer is provided for forward compatibility.

## After Installation

1. Launch Root Communications
2. Open **Settings** (gear icon)
3. Look for the **Uprooted** section in the sidebar
4. Configure features from the settings pages
