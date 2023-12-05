// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

// Chainlink Price Address List Page:
// https://docs.chain.link/data-feeds/price-feeds/addresses?network=ethereum&page=1
// 注意：上面的网页需要VPN才能看见地址。

// Oracle Address:
// https://etherscan.io/address/0xF4030086522a5bEEa4988F8cA5B36dbC97BeE88c


pragma solidity ^0.8.0;

import "./IChainlinkPrice.sol";
import "./Stringutils.sol";

contract ChainlinkPrice is IChainlinkPrice {
    using Stringutils for *;


    // 得到价格
    function getRoundPrice(address _aggregator, uint _timeline) external override view returns (int256 price_, uint time_, uint80 roundId_) {
        require(_timeline <= block.timestamp, "P1");
        AggregatorV2V3Interface cl = AggregatorV2V3Interface(_aggregator);
       
        if (block.timestamp == _timeline) {
            (roundId_, price_, time_, ,) = cl.latestRoundData();
            require(0 < price_, "P2");
            return  (price_, time_, roundId_) ;
        }
        else {
            uint80 LatestRound = uint80(cl.latestRound());
            for(uint80 r = LatestRound; 1 < r; r--) {
                uint256 startedAt;      //  uint256 updatedAt,
                (roundId_, price_, startedAt, , ) = cl.getRoundData(r);              
                if (startedAt <=  _timeline) {
                    require(0 < price_, "P3");
                    return  (price_, startedAt, roundId_) ;
                }
            }
        }

        require(1==2, "P4");
    }
    

    // 得到价格的小数位
    function getDecimals (address _aggregator) external override view returns (uint8) {
        AggregatorV2V3Interface cl = AggregatorV2V3Interface(_aggregator);
        return cl.decimals();
    }
  
    // 得到交易对信息， 例如 BTC/USD ， ETH/USD， 等等
    function getDescription (address _aggregator) external override view returns (string memory) {
        AggregatorV2V3Interface cl = AggregatorV2V3Interface(_aggregator);
        return cl.description();
    }

    function getDescriptionToken (address _aggregator) public override view returns (string memory desc_, address token0_, address token1_) {
        AggregatorV2V3Interface cl = AggregatorV2V3Interface(_aggregator);
        desc_ = cl.description();
        Stringutils.slice memory s = desc_.toSlice();
        Stringutils.slice memory delim = "/".toSlice();
        string[] memory parts = new string[](s.count(delim) + 1);
        for(uint i = 0; i < parts.length; i++) {
            parts[i] = s.split(delim).toString();
        }
        // token0_ = string2Address(string(abi.encodePacked(parts[0])));
        // token1_ = string2Address(string(abi.encodePacked(parts[1])));
        token0_ = string2Address(parts[0]);
        token1_ = string2Address(parts[1]);
    }

    function checkPair(address _aggregator, address _token0, address _token1) external override view returns (bool isOK_) 
    {
        (isOK_, ) = checkPairDetail(_aggregator, _token0, _token1);
    }

    function checkPairDetail(address _aggregator, address _token0, address _token1) public override view returns (bool isOK_, string memory result_) {
        (string memory desc_, address token0_, address token1_) = getDescriptionToken (_aggregator);
        isOK_ = (_token0 == token0_) && (_token1 == token1_);
        result_ = desc_; 
    }

    function address2String(address _token) public pure returns (string memory result_) {
        string memory r = string(abi.encodePacked(_token));
        result_ = r; 
    }


    function string2Address(string memory _s) public pure returns (address result_) {
        address r = address(bytes20(bytes(_s)));
        result_ = r; 
    }

    // 1.	AggregatorInterface接口给我们提供了5个方法供我们使用分别是：
    // 2.	latestAnswer() 最新的聚合结果
    // 3.	latestTimestamp() 最新一次聚合的时间戳
    // 4.	latestRound() 最新一次聚合的轮次号
    // 5.	getAnswer(uint256 roundId) 通过轮次号获取历史结果
    // 6.	getTimestamp(uint256 roundId) 通过轮次号获取历史时间戳

}