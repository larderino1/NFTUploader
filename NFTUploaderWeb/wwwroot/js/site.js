window.userWalletAddress = null;

let ethereum;

const loginButton = document.getElementById('metamask_login');
const userWallet = document.getElementById('userWallet');
const currentWalletAddress = $('#currentWalletAddress');

async function init() {
    // Check if metamask is installed
    if (typeof window.ethereum === 'undefined') {
        window.alert("Please install Metamask to connect to the blockchain.");
        return;
    }

    ethereum = window.ethereum;

    // Connect to provider
    await ethereum.request({ method: 'eth_requestAccounts' });

    if (ethereum.isConnected() === false) {
        window.alert("Please connect to Metamask.");
        return;
    }

    // Listen to account and network changes
    ethereum.on('accountsChanged', handleAccountsChanged);

    // Check if user is already logged in
    const accounts = await ethereum.request({ method: 'eth_accounts' });
    if (accounts.length !== 0) {
        handleAccountsChanged(accounts);
    }
}

async function handleAccountsChanged(accounts) {
    if (accounts.length === 0) {
        // user is logged out
        window.userWalletAddress = null;

        currentWalletAddress.text('');
        loginButton.removeEventListener('click', signOutOfMetaMask);
        loginButton.addEventListener('click', loginWithMetaMask);
    }
    else {
        // user is logged in
        window.userWalletAddress = accounts[0];

        currentWalletAddress.text('Connected wallet : ' + window.userWalletAddress);
        loginButton.removeEventListener('click', loginWithMetaMask);
        loginButton.addEventListener('click', signOutOfMetaMask);
    }
}

async function loginWithMetaMask() {
    const accounts = await ethereum.request({ method: 'eth_requestAccounts' }).catch((e) => {
        console.error(e.message);
        return;
    });

    if (!accounts) {
        return;
    }

    handleAccountsChanged(accounts);
}

async function signOutOfMetaMask() {
    await handleAccountsChanged([]);
}

window.addEventListener('DOMContentLoaded', () => {
    init();
});
