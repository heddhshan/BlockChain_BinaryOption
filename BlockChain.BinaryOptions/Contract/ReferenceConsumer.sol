pragma solidity ^0.8.0;

import "@chainlink/contracts/src/v0.4/interfaces/AggregatorInterface.sol";

contract ReferenceConsumer {
  AggregatorInterface internal ref;

  constructor(address _aggregator) public {
    ref = AggregatorInterface(_aggregator);
  }

  function getLatestAnswer() public view returns (int256) {
    return ref.latestAnswer();
  }

  function getLatestTimestamp() public view returns (uint256) {
    return ref.latestTimestamp();
  }

  function getLatestRound() public view returns (uint256) {
    return ref.getLatestRound();
  }

  function getPreviousAnswer(uint256 _backRound) public view returns (int256 _previousAnswer) {
    uint256 latest = ref.latestRound();
    require(_backRound <= latest, "Not enough history");
    return ref.getAnswer(latest - _backRound);
  }

  function getPreviousTimestamp(uint256 _backRound) public view returns (uint256 _timestamp) {
    uint256 latest = ref.latestRound();
    require(_back <= latest, "Not enough history");
    return ref.getTimestamp(latest - _backRound);
  }


  // 1.	AggregatorInterface接口给我们提供了5个方法供我们使用分别是：
  // 2.	latestAnswer() 最新的聚合结果
  // 3.	latestTimestamp() 最新一次聚合的时间戳
  // 4.	latestRound() 最新一次聚合的轮次号
  // 5.	getAnswer(uint256 roundId) 通过轮次号获取历史结果
  // 6.	getTimestamp(uint256 roundId) 通过轮次号获取历史时间戳


  // 通过历史时间 查找得到 对应的 RoundId
  function getHisRoundId(uint256 _hisTimestamp) public view returns (uint256 _hisRoundId) {
    require(_hisTimestamp < block.timestamp, "T1");
    uint256 latest = ref.latestRound();
    for(uint r = latest; 1 < r; r--) {
      uint t0 = getPreviousTimestamp(latest - r);
      uint t1 =  getPreviousTimestamp(latest - r - 1);
      if (t1 <=  _hisTimestamp && _hisTimestamp < t0) {
        return t1;
      }
    }
    require(1 != 1, "T2");
  }

  function getHisAnswer(uint256 _hisTimestamp) public view returns (int256 _hisAnswer) {
      uint roundid = getPreviousRound(_hisTimestamp);
      return ref.getAnswer(roundid); 
  }

}