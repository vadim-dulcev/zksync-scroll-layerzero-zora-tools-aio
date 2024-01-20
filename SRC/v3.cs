using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Nethereum.Uniswap.Contracts.UniswapV3Pool.ContractDefinition;

namespace Nethereum.Uniswap.Contracts.UniswapV3Pool
{
    public partial class UniswapV3PoolService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, UniswapV3PoolDeployment uniswapV3PoolDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<UniswapV3PoolDeployment>().SendRequestAndWaitForReceiptAsync(uniswapV3PoolDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, UniswapV3PoolDeployment uniswapV3PoolDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<UniswapV3PoolDeployment>().SendRequestAsync(uniswapV3PoolDeployment);
        }

        public static async Task<UniswapV3PoolService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, UniswapV3PoolDeployment uniswapV3PoolDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, uniswapV3PoolDeployment, cancellationTokenSource);
            return new UniswapV3PoolService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public UniswapV3PoolService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> BurnRequestAsync(BurnFunction burnFunction)
        {
            return ContractHandler.SendRequestAsync(burnFunction);
        }

        public Task<TransactionReceipt> BurnRequestAndWaitForReceiptAsync(BurnFunction burnFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFunction, cancellationToken);
        }

        public Task<string> BurnRequestAsync(int tickLower, int tickUpper, BigInteger amount)
        {
            var burnFunction = new BurnFunction();
            burnFunction.TickLower = tickLower;
            burnFunction.TickUpper = tickUpper;
            burnFunction.Amount = amount;

            return ContractHandler.SendRequestAsync(burnFunction);
        }

        public Task<TransactionReceipt> BurnRequestAndWaitForReceiptAsync(int tickLower, int tickUpper, BigInteger amount, CancellationTokenSource cancellationToken = null)
        {
            var burnFunction = new BurnFunction();
            burnFunction.TickLower = tickLower;
            burnFunction.TickUpper = tickUpper;
            burnFunction.Amount = amount;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(burnFunction, cancellationToken);
        }

        public Task<string> CollectRequestAsync(CollectFunction collectFunction)
        {
            return ContractHandler.SendRequestAsync(collectFunction);
        }

        public Task<TransactionReceipt> CollectRequestAndWaitForReceiptAsync(CollectFunction collectFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(collectFunction, cancellationToken);
        }
    }
}