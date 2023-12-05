// SPDX-License-Identifier: NoLicense
pragma solidity ^0.8.0;

abstract contract NoDelegateCall {
    address private immutable me;

    constructor() {
        me = address(this);
    }

    modifier WhenNotDelegateCall() {
        require(address(this) == me, "DC");
        _;
    }
}
