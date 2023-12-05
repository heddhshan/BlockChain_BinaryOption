// SPDX-License-Identifier: BUSL-1.1
// author: he.d.d.shan@hotmail.com 

pragma solidity ^0.8.0;


interface IChainlinkPrice
{
    function getRoundPrice(address _aggregator, uint _time) external view returns (int256 price_, uint time_, uint80 roundId_);
    function getDescription (address _aggregator) external view returns (string memory) ;    
    function getDecimals (address _aggregator) external view returns (uint8);
    function checkPair(address _aggregator, address _token0, address _token1) external view returns (bool isOK_);

    function getDescriptionToken (address _aggregator) external view returns (string memory desc_, address token0_, address token1_);
    function checkPairDetail(address _aggregator, address _token0, address _token1) external view returns (bool isOK_, string memory result_);

    function address2String(address _token) external pure returns (string memory result_);
    function string2Address(string memory _s) external pure returns (address result_);

}


interface AggregatorInterface {
  function latestAnswer() external view returns (int256);
  function latestTimestamp() external view returns (uint256);
  function latestRound() external view returns (uint256);
  function getAnswer(uint256 roundId) external view returns (int256);
  function getTimestamp(uint256 roundId) external view returns (uint256);

  event AnswerUpdated(int256 indexed current, uint256 indexed roundId, uint256 updatedAt);
  event NewRound(uint256 indexed roundId, address indexed startedBy, uint256 startedAt);
}

interface AggregatorV3Interface {

  function decimals() external view returns (uint8);
  function description() external view returns (string memory);
  function version() external view returns (uint256);

  // getRoundData and latestRoundData should both raise "No data present"
  // if they do not have data to report, instead of returning unset values
  // which could be misinterpreted as actual reported values.
  function getRoundData(uint80 _roundId)
    external
    view
    returns (
      uint80 roundId,
      int256 answer,
      uint256 startedAt,
      uint256 updatedAt,
      uint80 answeredInRound
    );
  function latestRoundData()
    external
    view
    returns (
      uint80 roundId,
      int256 answer,
      uint256 startedAt,
      uint256 updatedAt,
      uint80 answeredInRound
    );

}

interface AggregatorV2V3Interface is AggregatorInterface, AggregatorV3Interface
{
}