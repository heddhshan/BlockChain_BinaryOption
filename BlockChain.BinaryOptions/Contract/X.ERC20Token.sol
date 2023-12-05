// SPDX-License-Identifier: BUSL-1.1

pragma solidity ^0.8.0;

// refrence https://github.com/OpenZeppelin/openzeppelin-contracts

library Address { 
    function isContract(address account) internal view returns (bool) {
        uint256 size;
        assembly { size := extcodesize(account) }
        return size > 0;
    }
}


contract Context {
    // constructor () { }

    function _msgSender() internal view returns (address payable) {
        return payable(msg.sender);
    }
}

interface IERC20 {
    function totalSupply() external view returns (uint256);
    function decimals() external view returns (uint8);    
    function balanceOf(address account) external view returns (uint256);
    function transfer(address recipient, uint256 amount) external returns (bool);
    function allowance(address owner, address spender) external view returns (uint256);
    function approve(address spender, uint256 amount) external returns (bool);
    function transferFrom(address sender, address recipient, uint256 amount) external returns (bool);
    event Transfer(address indexed from, address indexed to, uint256 value);
    event Approval(address indexed owner, address indexed spender, uint256 value);
}


library SafeERC20 {
    using Address for address;

    function safeTransfer(IERC20 token, address to, uint256 value) internal {
        callOptionalReturn(token, abi.encodeWithSelector(token.transfer.selector, to, value));
    }

    function safeTransferFrom(IERC20 token, address from, address to, uint256 value) internal {
        callOptionalReturn(token, abi.encodeWithSelector(token.transferFrom.selector, from, to, value));
    }

    function safeApprove(IERC20 token, address spender, uint256 value) internal {      
        require((value == 0) || (token.allowance(address(this), spender) == 0),
            "5"
        );
        callOptionalReturn(token, abi.encodeWithSelector(token.approve.selector, spender, value));
    }

    function callOptionalReturn(IERC20 token, bytes memory data) private {
        require(address(token).isContract(), "6");
        (bool success, bytes memory returndata) = address(token).call(data);
        require(success, "7");
        if (returndata.length > 0) { // Return data is optional
            require(abi.decode(returndata, (bool)), "8");
        }
    }
}


abstract contract ERC20 is Context, IERC20 {
    // string public name = 'DAI Token';       
    // string public symbol = 'DAI';               //当作DAI的测试币

    uint8 internal _decimals = 18;                //18位
    function decimals() external view override returns (uint8) {
        return _decimals;
    }

    mapping (address => uint256) private _balances;
    mapping (address => mapping (address => uint256)) private _allowances;
    uint256 private _totalSupply = 0;

    function totalSupply() public view override returns (uint256) {
        return _totalSupply;
    }

    function balanceOf(address account) public view override returns (uint256) {
        return _balances[account];
    }
   
    function transfer(address recipient, uint256 amount) public  override returns (bool) {
        _transfer(_msgSender(), recipient, amount);
        return true;
    }
   
    function allowance(address owner, address spender) public view override returns (uint256) {
        return _allowances[owner][spender];
    }
   
    function approve(address spender, uint256 amount) public  override returns (bool) {
        _approve(_msgSender(), spender, amount);
        return true;
    }

    
    function transferFrom(address sender, address recipient, uint256 amount) public override returns (bool) {
        _transfer(sender, recipient, amount);
        _approve(sender, _msgSender(), _allowances[sender][_msgSender()] - (amount));
        return true;
    }
  
    function _transfer(address sender, address recipient, uint256 amount) internal  {
        require(sender != address(0), "ERC20: transfer from the zero address");
        require(recipient != address(0), "ERC20: transfer to the zero address");
        
        _balances[sender] = _balances[sender] - (amount);
        _balances[recipient] = _balances[recipient] + (amount);
        emit Transfer(sender, recipient, amount);
    }
    
    function _mint(address account, uint256 amount) internal  {
        require(account != address(0), "ERC20: mint to the zero address");
      
        _totalSupply = _totalSupply + (amount);
        _balances[account] = _balances[account] + (amount);
        emit Transfer(address(0), account, amount);
    }
    
    function burn(uint256 amount) public {
        _burn(_msgSender(), amount);
    }

    function _burn(address account, uint256 amount) internal  {
        require(account != address(0), "ERC20: burn from the zero address");

        _balances[account] = _balances[account] - (amount);
        _totalSupply = _totalSupply - (amount);
        emit Transfer(account, address(0), amount);
    }

    function _approve(address owner, address spender, uint256 amount) internal {
        require(owner != address(0), "ERC20: approve from the zero address");
        require(spender != address(0), "ERC20: approve to the zero address");
        _allowances[owner][spender] = amount;
        emit Approval(owner, spender, amount);
    }

  
}

// interface INextErc20 {
//     function mintByOldErc20(address _user, uint _amount) external;
// }

// contract UpgradeErc20 is ERC20, INextErc20 {
//     address public NextErc20;
//     function setNextErc20(address _token) external  {   //onlyAdmin
//         NextErc20 = _token;
//     }

//     event OnUpgrade(address _owner, uint _amount, uint _nexterc20blance);

//     function Upgrade(uint _amount) public returns (uint _nexterc20blance) {
//         require(NextErc20 != address(0), "U1");
//         burn(_amount);
//         INextErc20(NextErc20).mintByOldErc20(msg.sender, _amount);
//         _nexterc20blance = IERC20(NextErc20).balanceOf(msg.sender);
//         emit OnUpgrade(msg.sender, _amount, _nexterc20blance);
//     }
// }

// // 这个 ERC20 通用 Token， 用于测试
// contract UsdtToken is ERC20 {
    
//     string public name = 'Usdt Token';       
//     string public symbol = 'Usdt';               //当作DAI的测试币
//     //   uint8 private _decimals = 6;               //18位

//     constructor() {
//         _decimals = 6;
//         _mint(msg.sender,  (1e9) * (10 ** uint256(_decimals)));        
//     }

//     // 谁都可用挖矿，用于测试
//     function TestMint() external  {
//          _mint(msg.sender,  (1e9) * (10 ** uint256(_decimals)));        
//     }
// }


// // 这个 ERC20 通用 Token， 用于测试
// contract DAIToken is ERC20 {
    
//     string public name = 'DAI Token';       
//     string public symbol = 'DAI';               //当作DAI的测试币
//     //   uint8 private _decimals = 6;               //18位

//     constructor() {
//         _decimals = 18;
//         _mint(msg.sender,  (1e9) * (10 ** uint256(_decimals)));        
//     }

//     // 谁都可用挖矿，用于测试
//     function TestMint() external  {
//          _mint(msg.sender,  (1e9) * (10 ** uint256(_decimals)));        
//     }


// }

interface ITest {
    function TestMint() external;
}

// TokenA
contract TokenA is ERC20, ITest {
    string public name = 'TokenA';       
    string public symbol = 'TA';             

    constructor() {
        _decimals = 18;
        _mint(address(1),  (1e18) * (10 ** uint256(_decimals)));        
        _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }

    // 谁都可用挖矿，用于测试
    function TestMint() external override {
         _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }
}


// TokenB
contract TokenB is ERC20, ITest{
    string public name = 'TokenB';       
    string public symbol = 'TB';              

    constructor() {
        _decimals = 18;
        _mint(address(1),  (1e18) * (10 ** uint256(_decimals)));        
        _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }

    // 谁都可用挖矿，用于测试
    function TestMint() external override {
         _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }
}


// TokenC
contract TokenC is ERC20, ITest{
    string public name = 'TokenC';       
    string public symbol = 'TC';              

    constructor() {
        _decimals = 9;
        _mint(address(1),  (1e18) * (10 ** uint256(_decimals)));        
        _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }

    // 谁都可用挖矿，用于测试
    function TestMint() external override {
         _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }
}


// PlayToken
contract PlayToken is ERC20, ITest {
    string public name = 'PlayToken';       
    string public symbol = 'PT';              

    constructor() {
        _decimals = 6;
        _mint(address(1),  (1e18) * (10 ** uint256(_decimals)));        
        _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }

    // 谁都可用挖矿，用于测试
    function TestMint() external override  {
         _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }
}



// BetToken
contract BetToken is ERC20, ITest {
    string public name = 'BetToken';       
    string public symbol = 'BT';              

    constructor() {
        _decimals = 6;
        _mint(address(1),  (1e18) * (10 ** uint256(_decimals)));        
        _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }

    // 谁都可用挖矿，用于测试
    function TestMint() external override  {
         _mint(msg.sender,  (1e18) * (10 ** uint256(_decimals)));        
    }
}

